﻿using System.Collections.Generic;

namespace ArkNet.Core
{
	public class AccountApi
	{
		public string Address { get; set; }

		public string PublicKey { get; set; }

		public long Balance { get; set; }

		public string Username { get; set; }

		public int Rate { get; set; }

		public List<string> Votes { get; set; }

		public bool ApplyTransaction(TransactionApi transaction)
		{
			Balance -= transaction.Amount + transaction.Fee;
			return Balance > -1;
		}

		public bool UndoTransaction(TransactionApi transaction)
		{
			Balance += transaction.Amount + transaction.Fee;
			return Balance > -1;
		}

		public Verification VerifyTransaction(TransactionApi transaction)
		{
			var v = new Verification();
			/*if (Balance >= transaction.Amount + transaction.Fee)
				v.AddError(string.Format("Account %1 does not have enough balance: %2", Address, Balance));*/
			// TODO: many things

			return v;
		}
	}
}