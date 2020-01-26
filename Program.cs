using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using System.Numerics;

namespace SimpleMinerSim
{
	class Program
	{
		public static string userName;
		static string genesisBlockHash = "000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f";
		
		static string GenerateBlockTemplate(int length, string prevBlockHash)
		{
			var chars = "abcdef0123456789";
			var stringChars = new char[length];
			var random = new Random();

			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			var finalString = new String(stringChars);
			finalString = prevBlockHash + ">>>" + finalString; // '>>>' makes it easy to differentiate between the block header with the prevhash and the actual block contents.
			return finalString;
		}

		private static string ByteArrayToHex(byte[] barray)
		{
			char[] c = new char[barray.Length * 2];
			byte b;
			for (int i = 0; i < barray.Length; ++i)
			{
				b = ((byte)(barray[i] >> 4));
				c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
				b = ((byte)(barray[i] & 0xF));
				c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
			}
			return new string(c);
		}

		static void Main()
		{
			userName = Environment.UserName;
			Disk.SetupDataDir();

			if(Disk.LoadBestBlockHash() == "")
			{
				Disk.SaveBlockHash(genesisBlockHash);
				Disk.SaveBlockHeight(0);
			}
			
			Console.ForegroundColor = ConsoleColor.Green;

			string prevBlockHash = Disk.LoadBestBlockHash();
			int blockTime = 30; //in seconds
			int difficulty = 5; //number of leading zeros required for a valid block
			int validationCounter = 0;
			string candidateBlock;
			string candidateBlockNonced;
			string hashedBlock;
			BigInteger nonce = new BigInteger(1);
			ulong blockHeight = Disk.LoadBestBlockHeight();
			byte[] arrayToHash;
			Random r = new Random();
			Console.WriteLine("                                       SimCoin Mining Simulator");
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Network Difficulty: " + difficulty + " leading zeros");
			Console.ForegroundColor = ConsoleColor.Yellow;
			SHA256Managed Miner = new SHA256Managed();
			candidateBlock = GenerateBlockTemplate(r.Next(16, 64), prevBlockHash);

			while (true)
			{	
				candidateBlockNonced = candidateBlock + nonce;
				arrayToHash = Encoding.ASCII.GetBytes(candidateBlockNonced);
				hashedBlock = ByteArrayToHex((Miner.ComputeHash(arrayToHash)));

				for (int i = 0; i < difficulty; i++)
				{ 
					if (hashedBlock[i] == '0') validationCounter++;
				}

				if (validationCounter == difficulty)
				{
					
					prevBlockHash = hashedBlock;
					blockHeight++;
					Disk.SaveBlockHash(hashedBlock);
					Disk.SaveBlockHeight(blockHeight);
					Console.WriteLine("Block " + "#" + blockHeight + " found! Nonce:" + nonce + " Hash:" + hashedBlock.ToLower());
					nonce = 0;
					candidateBlock = GenerateBlockTemplate(r.Next(64, 64), prevBlockHash);
					
				}
				nonce++;
				validationCounter = 0;
				/*if (nonce % 1000000 == 0)
				{
					Console.WriteLine("nonces tried:" + nonce/1000000 + "M");
					Console.WriteLine(candidateBlockNonced);
				} */
				//Console.WriteLine(hashedBlock);
				
			}
		}
	}
}
