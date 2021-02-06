using System;
using System.Collections;
using System.Collections.Generic;

namespace Expenses.Contracts.HealthChecks
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<HealthCheck> HealthChecks { get; set; }
        public TimeSpan Duration { get; set; }
    }
}