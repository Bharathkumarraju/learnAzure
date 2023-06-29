using System;
using System.Collections.Generic;
using System.Reflection;
using Benday.YamlDemoApp.Api.DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.DomainModels
{
    public class DomainModelFieldTester<T> where T : DomainModelBase
    {
        private readonly T _systemUnderTest;

        public DomainModelFieldTester(T instance)
        {
            _systemUnderTest = instance ?? throw new ArgumentNullException(nameof(instance), "Argument cannot be null.");
        }

        public void RunChangeTrackingTestsForValueTypeProperties()
        {
            Assert.IsFalse(_systemUnderTest.HasChanges(),
            "System under test should not report HasChanges() true at start of test.");

            var typeUnderTest = typeof(T);

            var allProperties = typeUnderTest.GetProperties();

            var propertiesToTest = new List<PropertyInfo>();

            foreach (var prop in allProperties)
            {
                if (prop.PropertyType != typeof(string) && prop.PropertyType.IsValueType == false)
                {
                    continue;
                }

                if (prop.GetSetMethod() != null)
                {
                    var getter = prop.GetGetMethod();
                    if (getter != null && getter.IsPublic == true)
                    {
                        propertiesToTest.Add(prop);
                    }
                }
            }

            foreach (var prop in propertiesToTest)
            {
                AssertPropertyReturnsToOriginalValueOnUndoChanges(prop);
            }

            foreach (var prop in propertiesToTest)
            {
                AssertPropertyReturnsToAcceptedChangeOnUndoChanges(prop);
            }
        }

        private void AssertPropertyReturnsToOriginalValueOnUndoChanges(PropertyInfo propertyToTest)
        {
            Assert.IsFalse(_systemUnderTest.HasChanges(),
            "System under test should not have changes before checking property.");

            var getter = propertyToTest.GetGetMethod();
            var setter = propertyToTest.GetSetMethod();

            var original = GetValue(getter);

            ModifyValue(propertyToTest, setter, getter);

            Assert.IsTrue(_systemUnderTest.HasChanges(),
            $"System under test should have changes after modifying {propertyToTest.Name} property.");

            var current = GetValue(getter);

            Assert.AreNotEqual<object>(original, current,
            $"Property value for {propertyToTest.Name} did not change when modified.");

            _systemUnderTest.UndoChanges();

            current = GetValue(getter);

            Assert.AreEqual<object>(original, current,
            $"Property value for {propertyToTest.Name} did not revert to original when undo changes was called.");

            Assert.IsFalse(_systemUnderTest.HasChanges(),
            "System under test should not have changes after UndoChanges() was called.");
        }

        private void AssertPropertyReturnsToAcceptedChangeOnUndoChanges(PropertyInfo propertyToTest)
        {
            Assert.IsFalse(_systemUnderTest.HasChanges(),
            "System under test should not have changes before checking property.");

            var getter = propertyToTest.GetGetMethod();
            var setter = propertyToTest.GetSetMethod();

            ModifyValue(propertyToTest, setter, getter);

            Assert.IsTrue(_systemUnderTest.HasChanges(),
            "System under test should have changes after modification.");

            _systemUnderTest.AcceptChanges();

            var original = GetValue(getter);

            Assert.IsFalse(_systemUnderTest.HasChanges(),
            "System under test should not have changes after AcceptChanges() was called.");

            ModifyValue(propertyToTest, setter, getter);

            var current = GetValue(getter);

            Assert.AreNotEqual<object>(original, current,
            $"Property value for {propertyToTest.Name} did not change when modified.");

            _systemUnderTest.UndoChanges();

            current = GetValue(getter);

            Assert.AreEqual<object>(original, current,
            $"Property value for {propertyToTest.Name} did not revert to accepted version when undo changes was called.");

            Assert.IsFalse(_systemUnderTest.HasChanges(),
            "System under test should not have changes after UndoChanges() was called.");
        }

        private void ModifyValue(PropertyInfo prop, MethodInfo setter, MethodInfo getter)
        {
            object modifiedValue;

            if (prop.PropertyType == typeof(int))
            {
                var current = (int)GetValue(getter);

                modifiedValue = current - 1;
            }
            else if (prop.PropertyType == typeof(double))
            {
                var current = (double)GetValue(getter);

                modifiedValue = current - 1;
            }
            else if (prop.PropertyType == typeof(float))
            {
                var current = (float)GetValue(getter);

                modifiedValue = current - 1;
            }
            else if (prop.PropertyType == typeof(string))
            {
                modifiedValue = Guid.NewGuid().ToString();
            }
            else if (prop.PropertyType == typeof(DateTime))
            {
                var rnd = new Random().Next(10, 100);

                modifiedValue = DateTime.Now.AddYears(10).AddMinutes(rnd);
            }
            else if (prop.PropertyType == typeof(bool))
            {
                var current = (bool)GetValue(getter);

                modifiedValue = !current;
            }
            else
            {
                throw new InvalidOperationException(
                $"Type '{prop.PropertyType.Name}' for '{prop.Name}' property not supported.");
            }

            setter.Invoke(_systemUnderTest, new object[] { modifiedValue });
        }

        private object GetValue(MethodInfo getter)
        {
            return getter.Invoke(_systemUnderTest, Array.Empty<object>());
        }
    }
}
