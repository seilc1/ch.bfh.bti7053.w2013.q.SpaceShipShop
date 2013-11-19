using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Authentication
{
	public class AuthenticationProvider
	{
		private readonly int _saltSize;
		private const int CharToByteRatio = 2;

		public static byte[] GetBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public static string GetString(byte[] bytes)
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}

		public AuthenticationProvider(int saltSize)
		{
			_saltSize = saltSize * CharToByteRatio;
		}

		public bool Validate(string password, string encryptedPassword, string salt)
		{
			using (var deriveBytes = new Rfc2898DeriveBytes(password, GetBytes(salt)))
			{
				byte[] newKey = deriveBytes.GetBytes(_saltSize);
				byte[] encByteKey = GetBytes(encryptedPassword);

				if (newKey.SequenceEqual(encByteKey))
				{
					return true;
				}
			}

			return false;
		}

		public string EncryptPassword(string password, out string salt)
		{
			byte[] saltArray, passwordArray;
			passwordArray = EncryptPassword(password, out saltArray);

			salt = GetString(saltArray);
			return GetString(passwordArray);
		}

		public byte[] EncryptPassword(string password, out byte[] salt)
		{
			using (var deriveBytes = new Rfc2898DeriveBytes(password, _saltSize))
			{
				salt = deriveBytes.Salt;
				return deriveBytes.GetBytes(_saltSize);
			}
		}
	}
}
