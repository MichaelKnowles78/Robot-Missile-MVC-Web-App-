using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Constraints;

namespace RobotMissileTest;

class ReturnsView : Constraint
{
    private readonly string _expected;

    public ReturnsView(string expected)
    {
        _expected = expected;
    }

    public override ConstraintResult ApplyTo<TActual>(TActual actual)
    {
        if (actual is not ViewResult result)
        {
            return new ConstraintResult(this, actual, ConstraintStatus.Error);
        }

        if (result.ViewName == _expected)
        {
            return new ConstraintResult(this, actual, ConstraintStatus.Success);
        }

        return new ConstraintResult(this, actual, ConstraintStatus.Failure);
    }
}
