using System;

namespace app.startup
{
  public delegate IRunAStartupStep ICreateStartupStep(Type step_type);
}