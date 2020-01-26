using SimpleMinerSim;
using System.IO;
public class Disk
{
	public static string dirPath = "C:/Users/" + Program.userName + "/AppData/Local/SimCoin/";
	public static string filePath = "C:/Users/" + Program.userName + "/AppData/Local/SimCoin/Hash.smc";
	public static string secondFilePath = "C:/Users/" + Program.userName + "/AppData/Local/SimCoin/Height.smc";

	public static void SetupDataDir()
	{
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
		}
	}

	public static void SaveBlockHash(string blockHash)
	{

		StreamWriter HashWriter = new StreamWriter(filePath);
		if (HashWriter != null)
		{
			HashWriter.BaseStream.Seek(0, 0);
			HashWriter.WriteLine(blockHash);
			HashWriter.Flush();
			HashWriter.Close();
		}
	}

	public static string LoadBestBlockHash()
	{
		if (File.Exists(filePath) == false) return "";
		StreamReader HashReader = new StreamReader(filePath);
		HashReader.BaseStream.Seek(0, 0);
		string blockHash = "";

		if (HashReader != null)
		{
			blockHash = HashReader.ReadLine();
			HashReader.Close();
		}

		return blockHash;
	}

	public static void SaveBlockHeight(ulong blockHeight)
	{
		StreamWriter NumWriter = new StreamWriter(secondFilePath);
		if (NumWriter != null)
		{
			NumWriter.BaseStream.Seek(0, 0);
			NumWriter.WriteLine(blockHeight);
			NumWriter.Flush();
			NumWriter.Close();
		}
	}

	public static ulong LoadBestBlockHeight()
	{
		if (File.Exists(filePath) == false) return 0;
		StreamReader NumReader = new StreamReader(secondFilePath);
		NumReader.BaseStream.Seek(0 , 0);
		ulong height = 0;

		if (NumReader != null)
		{
			height = ulong.Parse(NumReader.ReadLine());
			NumReader.Close();
		}

		return height;
	}
}
