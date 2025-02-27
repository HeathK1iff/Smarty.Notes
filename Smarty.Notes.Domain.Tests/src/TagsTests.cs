using NUnit.Framework.Internal;
using Smarty.Notes.Domain.Entities.Aggregates;
using Smarty.Notes.Domain.Exceptions;

namespace Smarty.Notes.Domain.Tests;

[TestFixture]
public class TagsTests
{
    [SetUp]
    public void Setup()
    {

    }

    [TestCase]
    public void Add_AlreadyExistTag_ResultThrowAlreadyExistException()
    {
        var actual = new Tags()
        {
            "test"
        };

        Assert.Throws<DuplicateException>(() => actual.Add("test"));
    }

    [TestCase]
    public void Add_AddEmptyToList_ResultThrowArgumentException()
    {
        var actual = new Tags();

        Assert.Throws<ArgumentException>(() => actual.Add(string.Empty));
    }


    [TestCase]
    public void Add_AddValueInEmptyList_ResultSuccess()
    {
        var actual = new Tags();
        var expected = new Tags() { "test" };

        actual.Add("test");

        Assert.That(actual, Is.EqualTo(expected).AsCollection);
    }

    [TestCase]
    public void Add_AddMultiplySeqValueInEmptyList_ResultSuccess()
    {
        var actual = new Tags();
        var expected = new Tags() { "test", "test1" };

        actual.Add("test");
        actual.Add("test1");

        Assert.That(actual, Is.EqualTo(expected).AsCollection);
    }

    [TestCase]
    public void Remove_TryNotExistingItem_ResultThrowNotFoundException()
    {
        var actual = new Tags() { "test" };

        Assert.Throws<NotFoundException>(() => actual.Remove("test2"));
    }
}
