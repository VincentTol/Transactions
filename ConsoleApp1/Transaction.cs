using System;
using System.Collections.Generic;

namespace TransactionAssignment
{
    // Public interface for transaction operations
    public interface ITransaction
    {
        void BeginTransaction();
        void Put(string key, int value);
        int? Get(string key);
        void Commit();
        void Rollback();
    }

    public class Transactions : ITransaction
    {
        // Main committed state
        private Dictionary<string, int> store = new Dictionary<string, int>();

        // Holds staged, uncommitted changes
        private Dictionary<string, int>? transactionChanges = null;

        private bool inTransaction = false;

        // Begin transaction
        public void BeginTransaction()
        {
            if (inTransaction)
                throw new InvalidOperationException("Transaction already in progress.");

            inTransaction = true;
            transactionChanges = new Dictionary<string, int>();
        }

        // Put (MUST be inside a transaction)
        public void Put(string key, int value)
        {
            if (!inTransaction)
                throw new InvalidOperationException("put() called without active transaction.");

            transactionChanges![key] = value;
        }

        // Get (NEVER sees uncommitted changes)
        public int? Get(string key)
        {
            if (store.ContainsKey(key))
                return store[key];

            return null;
        }

        // Commit (apply staged changes)
        public void Commit()
        {
            if (!inTransaction)
                throw new InvalidOperationException("commit() called without active transaction.");

            foreach (var pair in transactionChanges!)
            {
                store[pair.Key] = pair.Value;
            }

            transactionChanges = null;
            inTransaction = false;
        }

        // Rollback (discard staged changes)
        public void Rollback()
        {
            if (!inTransaction)
                throw new InvalidOperationException("rollback() called without active transaction.");

            transactionChanges = null;
            inTransaction = false;
        }
    }

}



