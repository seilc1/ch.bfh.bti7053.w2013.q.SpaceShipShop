using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uniques.Library.Authentication;

namespace Uniques.Tests
{
	[TestClass]
	public class AuthenticationProviderTest
	{
		private const int SaltSize = 20;

		private AuthenticationProvider Provider
		{
			get
			{
				return new AuthenticationProvider(SaltSize);
			}
		}

		[TestMethod]
		public void ByteCopyBidrectional()
		{
			const string src = "Source String #! Mit Spezialzeichen";
			byte[] byteResult = AuthenticationProvider.GetBytes(src);

			string revert = AuthenticationProvider.GetString(byteResult);

			Assert.AreEqual(src, revert);
		}

		[TestMethod]
		public void ByteCopyBidrectionalByte()
		{
			const string password = "hmb5CD84v6o5%Sj";

			byte[] salt;
			Provider.EncryptPassword(password, out salt);

			byte[] revert = AuthenticationProvider.GetBytes(AuthenticationProvider.GetString(salt));

			for (var i = 0; i < salt.Length; i++)
			{
				Assert.AreEqual(salt[i], revert[i]);
			}
		}

		[TestMethod]
		public void EncryptionTestRespectLength()
		{
			const string password = "hmb5CD84v6o5%Sj";
			string salt, salt2;

			string encryptedPassword = Provider.EncryptPassword(password, out salt);
			string encryptedPassword2 = Provider.EncryptPassword(password, out salt2);

			Assert.AreEqual(encryptedPassword.Length, SaltSize);
			Assert.AreEqual(salt.Length, SaltSize);

			Assert.AreNotEqual(password, encryptedPassword);
			Assert.AreNotEqual(encryptedPassword2, encryptedPassword);
			Assert.AreNotEqual(salt, salt2);

			Assert.IsTrue(Provider.Validate(password, encryptedPassword, salt));
		}

		[TestMethod]
		public void AuthenticationProviderUniqueTest()
		{
			string[] passwords = new[] { "hmb5CD84v6o5%Sj", "hmb5CD84v6o5%Sj", "hmb5CD84v6o5%Sj", "z,3VF!p{R<&iP+G", "@/!^I@n5//b6'~L" };
			List<string> Salts = new List<string>();
			List<string> Passwords = new List<string>();

			foreach (string password in passwords)
			{
				string salt;
				string encryptedPassword = Provider.EncryptPassword(password, out salt);

				Assert.IsFalse(Salts.Contains(salt));
				Assert.IsFalse(Passwords.Contains(encryptedPassword));

				Salts.Add(salt);
				Passwords.Add(password);

				Assert.AreEqual(encryptedPassword.Length, SaltSize);
				Assert.AreEqual(salt.Length, SaltSize);

				Assert.AreNotEqual(password, encryptedPassword);

				Assert.IsTrue(Provider.Validate(password, encryptedPassword, salt));
			}
		}
	}
}
