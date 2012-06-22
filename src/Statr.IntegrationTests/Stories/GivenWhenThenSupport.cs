using System;
using NUnit.Framework;

namespace Statr.IntegrationTests.Stories
{
    public class GivenWhenThenSupport<T>
        where T : ScenarioContext, new()
    {
        private T currentContext;

        public GivenWhenThenSupport<T> Given(Action given)
        {
            currentContext = new T();
            given();
            return this;
        }

        public GivenWhenThenSupport<T> Given(Action<T> given)
        {
            currentContext = new T();
            given(currentContext);
            return this;
        }

        public GivenWhenThenSupport<T> When(Action when)
        {
            when();
            return this;
        }

        public GivenWhenThenSupport<T> When(Action<T> when)
        {
            when(currentContext);
            return this;
        }

        public GivenWhenThenSupport<T> Then(Action<T> then)
        {
            then(currentContext);
            return this;
        }

        public GivenWhenThenSupport<T> Then(Action then)
        {
            then();
            return this;
        }

        public GivenWhenThenSupport<T> And(Action<T> and)
        {
            and(currentContext);
            return this;
        }

        public GivenWhenThenSupport<T> And(Action and)
        {
            and();
            return this;
        }

        [TearDown]
        public void CleanUpCurrentContext()
        {
            if (currentContext != null)
            {
                currentContext.Dispose();
            }

            AfterTest();
        }

        public virtual void AfterTest()
        {

        }
    }
}
