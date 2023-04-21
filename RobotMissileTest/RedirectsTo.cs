using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Constraints;

namespace RobotMissileTest;

class RedirectsTo : Constraint
{
    private readonly string _expected;

    public RedirectsTo(string expected)
    {
        _expected = expected;
    }

    public override ConstraintResult ApplyTo<TActual>(TActual actual)
    {
        if (actual is not RedirectToActionResult redirect)
        {
            return new ConstraintResult(this, actual, ConstraintStatus.Error);
        }

        if (redirect.ActionName == _expected)
        {
            return new ConstraintResult(this, actual, ConstraintStatus.Success);
        }

        return new ConstraintResult(this, actual, ConstraintStatus.Failure);
    }
}
