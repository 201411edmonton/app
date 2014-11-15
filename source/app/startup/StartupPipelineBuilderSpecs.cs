using System;
using System.Collections.Generic;
using System.Linq;
using app.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using Machine.Specifications.Runner.Impl;

namespace app.startup

{
  [Subject(typeof(StartupPipelineBuilder))]
  public class StartupPipelineBuilderSpecs
  {
    public abstract class concern_for_initial_step : Observes<ISpecifyTheFirstStepInAStartupPipeline,
      StartupPipelineBuilder>
    {
    }

    public class when_provided_an_initial_step : concern_for_initial_step
    {
      Establish c = () =>
      {
        the_step = fake.an<IRunAStartupStep>();
        combined_step = fake.an<IRunAnAction>();

        depends.on<ICombineActions>((first, second) =>
        {
          first.ShouldBeAn<IRunAnAction>();
          second.ShouldEqual(the_step);
          return combined_step;
        });

        depends.on<ICreateStartupStep>(x =>
        {
          x.ShouldEqual(typeof(FirstStep));
          return the_step;
        });
        depends.on<IList<IRunAStartupStep>>(new List<IRunAStartupStep>());
      };

      Because b = () =>
        result = sut.running<FirstStep>();

      It returns_a_new_builder_with_the_first_step_created = () =>
        result.downcast_to<StartupPipelineBuilder>().step.ShouldEqual(combined_step);

      It allows_the_client_to_continue_adding_steps_to_the_pipeline = () =>
      {
        result.ShouldBeOfType<IAddExtraStepsToAStartupPipeline>();
        result.Equals(sut).ShouldBeFalse();
      };

      static IRunAStartupStep the_step;
      static IAddExtraStepsToAStartupPipeline result;
      static IRunAnAction combined_step;
    }

    public class when_specifying_successive_steps : concern_for_adding_steps
    {
      Establish c = () =>
      {
        first_step = fake.an<IRunAStartupStep>();
        second_step = fake.an<IRunAStartupStep>();
        combined_step = fake.an<IRunAnAction>();

        depends.on<ICombineActions>((first, second) =>
        {
          first.ShouldEqual(first_step);
          second.ShouldEqual(second_step);
          return combined_step;
        });

        depends.on<IRunAnAction>(first_step);
        depends.on<ICreateStartupStep>(x =>
        {
          x.ShouldEqual(typeof(SecondStep));
          return second_step;
        });
      };

      Because b = () =>
        result = sut.then<SecondStep>();

      It stores_the_step_created_by_the_step_factory_in_a_list_of_all_steps_to_run = () =>
        result.downcast_to<StartupPipelineBuilder>().step.ShouldEqual(combined_step);

      It returns_a_builder_to_continue_adding_steps = () =>
        result.ShouldNotEqual(sut);

      static IRunAStartupStep first_step;
      static IAddExtraStepsToAStartupPipeline result;
      static IRunAStartupStep second_step;
      static IRunAnAction combined_step;
    }

    public class when_the_last_step_is_specified : concern_for_adding_steps
    {
      Establish c = () =>
      {
        first_step = depends.on<IRunAnAction>();
        second_step = fake.an<IRunAStartupStep>();
        combined_step = fake.an<IRunAnAction>();

        depends.on<ICombineActions>((first,second) =>
        {
          first.ShouldEqual(first_step);
          second.ShouldEqual(second_step);
          return combined_step;
        });

        depends.on<ICreateStartupStep>(x =>
        {
          x.ShouldEqual(typeof(SecondStep));
          return second_step;
        });
      };

      Because b = () =>
        sut.finish_with<SecondStep>();

      It runs_all_of_the_steps_in_sequence = () =>
        combined_step.received(x => x.run());

      static IRunAnAction first_step;
      static IRunAStartupStep second_step;
      static IRunAnAction combined_step;
    }
  }

  public class SecondStep : IRunAStartupStep
  {
    public void run()
    {
    }
  }

  public abstract class concern_for_adding_steps : Observes<IAddExtraStepsToAStartupPipeline,
    StartupPipelineBuilder>
  {
  }

  public class FirstStep : IRunAStartupStep
  {
    public void run()
    {
      throw new NotImplementedException();
    }
  }

  public class StartupPipelineBuilder : ISpecifyTheFirstStepInAStartupPipeline,
    IAddExtraStepsToAStartupPipeline
  {
    public IRunAnAction step;
    ICreateStartupStep step_factory;
    ICombineActions combine_actions;

    public StartupPipelineBuilder(IRunAnAction step, ICreateStartupStep step_factory, ICombineActions combine_actions)
    {
      this.step = step;
      this.step_factory = step_factory;
      this.combine_actions = combine_actions;
    }

    public IAddExtraStepsToAStartupPipeline running<Step>() where Step : IRunAStartupStep
    {
      return combine_with<Step>();
    }

    IAddExtraStepsToAStartupPipeline combine_with<Step>() where Step : IRunAStartupStep
    {
      var new_step = step_factory(typeof(Step));
      return new StartupPipelineBuilder(combine_actions(step, new_step), step_factory, combine_actions);
    }

    public IAddExtraStepsToAStartupPipeline then<Step>() where Step : IRunAStartupStep
    {
      return combine_with<Step>();
    }

    public void finish_with<Step>() where Step : IRunAStartupStep
    {
      var last_step = step_factory(typeof(Step));

      combine_actions(step, last_step).run();
    }
  }

  public class NonStep : IRunAnAction
  {
    public void run()
    {
    }
  }
}
