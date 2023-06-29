using Benday.YamlDemoApp.Api.DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.DomainModels
{
    [TestClass]
    public class DomainModelFieldFixture
    {
        private const string INITIAL_VALUE = "ORIGINAL";
        private const string MODIFIED_VALUE = "MODIFIED";
        private const string ANOTHER_MODIFIED_VALUE = "ANOTHER MODIFIED";

        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private DomainModelField<string> _systemUnderTest;

        private DomainModelField<string> SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new DomainModelField<string>(INITIAL_VALUE);
                }

                return _systemUnderTest;
            }
        }

        [TestMethod]
        public void HasChanges_OnNewInstance_IsFalse()
        {
            // arrange

            // act

            // assert
            Assert.IsFalse(SystemUnderTest.HasChanges(), "HasChanges was wrong");
        }

        [TestMethod]
        public void HasChanges_WhenCurrentValueIsNullAndOriginalIsNull_IsFalse()
        {
            // arrange
            _systemUnderTest = new DomainModelField<string>(null);
            var expected = false;
            SystemUnderTest.Value = null;

            // act
            var actual = SystemUnderTest.HasChanges();

            // assert
            Assert.AreEqual<bool>(expected, actual, "HasChanges() was wrong");
        }

        [TestMethod]
        public void HasChanges_WhenCurrentValueIsNullAndOriginalIsNotNull_IsTrue()
        {
            // arrange
            _systemUnderTest = new DomainModelField<string>(INITIAL_VALUE);
            var expected = true;
            SystemUnderTest.Value = null;

            // act
            var actual = SystemUnderTest.HasChanges();

            // assert
            Assert.AreEqual<bool>(expected, actual, "HasChanges() was wrong");
        }

        [TestMethod]
        public void HasChanges_WhenCurrentValueIsNotNullAndOriginalIsNull_IsTrue()
        {
            // arrange
            _systemUnderTest = new DomainModelField<string>(null);
            var expected = true;
            SystemUnderTest.Value = MODIFIED_VALUE;

            // act
            var actual = SystemUnderTest.HasChanges();

            // assert
            Assert.AreEqual<bool>(expected, actual, "HasChanges() was wrong");
        }

        [TestMethod]
        public void HasChanges_OnModifiedInstance_IsTrue()
        {
            // arrange
            var expected = true;
            SystemUnderTest.Value = MODIFIED_VALUE;

            // act
            var actual = SystemUnderTest.HasChanges();

            // assert
            Assert.AreEqual<bool>(expected, actual, "HasChanges was wrong.");
        }

        [TestMethod]
        public void UndoChangesRevertsToOriginalAndIsDirtyIsFalse()
        {
            // arrange
            var expected = false;
            var originalValue = SystemUnderTest.Value;
            SystemUnderTest.Value = MODIFIED_VALUE;

            // act
            SystemUnderTest.UndoChanges();
            var actual = SystemUnderTest.HasChanges();

            // assert
            Assert.AreEqual<string>(originalValue, SystemUnderTest.Value, "Value didn't revert");
            Assert.AreEqual<bool>(expected, actual, "HasChanges was wrong.");
        }

        [TestMethod]
        public void AcceptChangesOnModifiedValueSetsIsDirtyToFalse()
        {
            // arrange
            var expected = false;
            SystemUnderTest.Value = MODIFIED_VALUE;

            // act
            SystemUnderTest.AcceptChanges();
            var actual = SystemUnderTest.HasChanges();

            // assert
            Assert.AreEqual<bool>(expected, actual, "HasChanges was wrong.");
        }

        [TestMethod]
        public void UndoChangesRevertsToValueAfterAcceptChangesAndIsDirtyIsFalse()
        {
            // arrange
            var expected = false;
            SystemUnderTest.Value = MODIFIED_VALUE;
            var originalValue = SystemUnderTest.Value;
            SystemUnderTest.AcceptChanges();
            SystemUnderTest.Value = ANOTHER_MODIFIED_VALUE;

            // act
            SystemUnderTest.UndoChanges();
            var actual = SystemUnderTest.HasChanges();

            // assert
            Assert.AreEqual<string>(originalValue, SystemUnderTest.Value, "Value didn't revert");
            Assert.AreEqual<bool>(expected, actual, "HasChanges was wrong.");
        }

        [TestMethod]
        public void ValueReturnsExpectedValue_AfterConstructor()
        {
            // arrange
            var expected = INITIAL_VALUE;

            // act
            var actual = SystemUnderTest.Value;

            // assert
            Assert.AreEqual<string>(expected, actual, "Value was wrong.");
        }

        [TestMethod]
        public void ValueReturnsExpectedValue_AfterModification()
        {
            // arrange
            var expected = MODIFIED_VALUE;
            SystemUnderTest.Value = MODIFIED_VALUE;

            // act
            var actual = SystemUnderTest.Value;

            // assert
            Assert.AreEqual<string>(expected, actual, "Value was wrong.");
        }
    }
}
