﻿using System;

namespace Authority.UnitOfWork
{
    public sealed class RequirementFailedException : Exception
    {
        public RequirementFailedException(int errorCode)
            : base(errorCode.ToString(), null)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; private set; }
    }
}
