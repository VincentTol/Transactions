using NUnit.Framework;
using TransactionAssignment;

[TestFixture]
public class SimpleKVStoreIntegrationTests
{
    private ITransaction db;

    [SetUp]
    public void Setup()
    {
        db = new Transactions();
    }

    [Test]
    public void Get_ShouldReturnNull_WhenKeyDoesNotExist()
    {
        Assert.IsNull(db.Get("missing"));
    }

    [Test]
    public void Get_ShouldReturnCommittedValue()
    {
        db.BeginTransaction();
        db.Put("a", 10);
        db.Commit();

        Assert.AreEqual(10, db.Get("a"));
    }

    [Test]
    public void Put_WithoutTransaction_ShouldThrow()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            db.Put("a", 5);
        });
    }

    [Test]
    public void Put_InsideTransaction_ShouldNotBeVisibleUntilCommit()
    {
        db.BeginTransaction();
        db.Put("x", 100);

        Assert.IsNull(db.Get("x"));

        db.Commit();
        Assert.AreEqual(100, db.Get("x"));
    }

    [Test]
    public void BeginTransaction_WhileAlreadyInTransaction_ShouldThrow()
    {
        db.BeginTransaction();
        Assert.Throws<InvalidOperationException>(() => db.BeginTransaction());
    }

    [Test]
    public void Commit_ShouldApplyAllStagedChanges()
    {
        db.BeginTransaction();
        db.Put("a", 1);
        db.Put("b", 2);
        db.Put("a", 99);

        db.Commit();

        Assert.AreEqual(99, db.Get("a"));
        Assert.AreEqual(2, db.Get("b"));
    }

    [Test]
    public void Rollback_ShouldDiscardAllStagedChanges()
    {
        db.BeginTransaction();
        db.Put("a", 10);
        db.Put("b", 20);
        db.Rollback();

        Assert.IsNull(db.Get("a"));
        Assert.IsNull(db.Get("b"));
    }

    [Test]
    public void Commit_WithoutTransaction_ShouldThrow()
    {
        Assert.Throws<InvalidOperationException>(() => db.Commit());
    }

    [Test]
    public void Rollback_WithoutTransaction_ShouldThrow()
    {
        Assert.Throws<InvalidOperationException>(() => db.Rollback());
    }

    [Test]
    public void MultipleTransactions_ShouldBehaveIndependently()
    {
        db.BeginTransaction();
        db.Put("a", 10);
        db.Commit();

        db.BeginTransaction();
        db.Put("a", 20);
        db.Rollback();

        Assert.AreEqual(10, db.Get("a"));
    }
}
