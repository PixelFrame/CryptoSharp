using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CryptoSharp
{
	public partial class MainForm : Form
	{
		#region MainForm Variables
		string Input;
		string Output;
		List<string> AlgorithmList = new List<string>();
		List<string> ModeList = new List<string>();
		List<byte> Key = new List<byte>();
		List<byte> IV = new List<byte>();
		int DataLength = 8;

		RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();

		CryptCore Core = new CryptCore();

		#endregion

		public MainForm()
		{
			InitializeComponent();
			Input = "";
			Output = "";
			AlgorithmList.Add("AES-128");
			AlgorithmList.Add("DES");
			AlgorithmList.Add("BASE64");
			cbAlgorithm.DataSource = AlgorithmList;
			ModeList.Add("ECB");
			ModeList.Add("CBC");
			ModeList.Add("CFB");
			ModeList.Add("OFB");
			ModeList.Add("PCBC");
			cbMode.DataSource = ModeList;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnAbout_Click(object sender, EventArgs e)
		{
			FormAbout about = new FormAbout();
			about.ShowDialog();
		}

		private void btnRandIV_Click(object sender, EventArgs e)
		{
			byte[] byteCsp = new byte[DataLength];
			csp.GetBytes(byteCsp);
			tbIV.Text = BitConverter.ToString(byteCsp);
		}

		private void btnRandKey_Click(object sender, EventArgs e)
		{
			byte[] byteCsp = new byte[DataLength];
			csp.GetBytes(byteCsp);
			tbKey.Text = BitConverter.ToString(byteCsp);
		}

		private void cbAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbAlgorithm.SelectedIndex)
			{
				case 0:
					DataLength = 16;
					btnRandIV.Enabled = true;
					btnRandKey.Enabled = true;
					tbIV.Enabled = true;
					tbKey.Enabled = true;
					cbMode.Enabled = true;
					break;
				case 1:
					DataLength = 8;
					btnRandIV.Enabled = true;
					btnRandKey.Enabled = true;
					tbIV.Enabled = true;
					tbKey.Enabled = true;
					cbMode.Enabled = true;
					break;
				case 2:
					btnRandIV.Enabled = false;
					btnRandKey.Enabled = false;
					tbIV.Enabled = false;
					tbKey.Enabled = false;
					cbMode.Enabled = false;
					break;
			}
			tbKey.Text = "";
			tbIV.Text = "";
		}

		private void tbIV_Validating(object sender, CancelEventArgs e)
		{
			const string pattern = @"^[a-fA-F0-9\-]+$";
			string content = ((TextBox)sender).Text;

			if (!Regex.IsMatch(content, pattern) && content != "")
			{
				string message = string.Format("INPUT NOT VALID\nPlease input {0}-byte HEX.\nYou can seperate it by \'-\'", DataLength);
				MessageBox.Show(message, "ERROR");
				tbIV.Text = "";
			}
		}

		private void tbKey_Validating(object sender, CancelEventArgs e)
		{
			const string pattern = @"^[a-fA-F0-9\-]+$";
			string content = ((TextBox)sender).Text;

			if (!Regex.IsMatch(content, pattern) && content != "")
			{
				string message = string.Format("INPUT NOT VALID\nPlease input {0}-byte HEX.\nYou can seperate it by \'-\'", DataLength);
				MessageBox.Show(message, "ERROR");
				tbKey.Text = "";
			}
		}

		private void btnEncrypt_Click(object sender, EventArgs e)
		{
			Input = tbInput.Text;
			Key = HexStringToBytes(tbKey.Text);
			IV = HexStringToBytes(tbIV.Text);
			
			switch (cbAlgorithm.SelectedIndex)
			{
				case 0:
					UpdateOutput(Core.AES_Crypt(false, Encoding.UTF8.GetBytes(Input), Key.ToArray(), IV.ToArray(), cbMode.SelectedIndex));
					break;
				case 1:
					UpdateOutput(Core.DES_Crypt(false, Encoding.UTF8.GetBytes(Input), BitConverter.ToUInt64(Key.ToArray(), 0), BitConverter.ToUInt64(IV.ToArray(), 0), cbMode.SelectedIndex));
					break;
				case 2:
					tbOutput.Text = Core.BASE64_Convert(false, Input);
					break;
			}
		}

		private void UpdateOutput(byte[] output)
		{
			if (ckbOutBASE64.Checked) tbOutput.Text = Core.BASE64_Convert(false, output);
			else tbOutput.Text = new string(Encoding.UTF8.GetChars(output));
		}

		private List<byte> HexStringToBytes(string hexString)
		{
			var flag = 0;
			var token = new byte();
			var bytes = new List<byte>();
			foreach (char c in hexString)
			{
				switch (c)
				{
					case '0':
						++flag; break;
					case '1':
						token ^= (byte)(0x1 << ((2 - ++flag) * 4)); break;
					case '2':
						token ^= (byte)(0x2 << ((2 - ++flag) * 4)); break;
					case '3':
						token ^= (byte)(0x3 << ((2 - ++flag) * 4)); break;
					case '4':
						token ^= (byte)(0x4 << ((2 - ++flag) * 4)); break;
					case '5':
						token ^= (byte)(0x5 << ((2 - ++flag) * 4)); break;
					case '6':
						token ^= (byte)(0x6 << ((2 - ++flag) * 4)); break;
					case '7':
						token ^= (byte)(0x7 << ((2 - ++flag) * 4)); break;
					case '8':
						token ^= (byte)(0x8 << ((2 - ++flag) * 4)); break;
					case '9':
						token ^= (byte)(0x9 << ((2 - ++flag) * 4)); break;
					case 'A':
					case 'a':
						token ^= (byte)(0xA << ((2 - ++flag) * 4)); break;
					case 'B':
					case 'b':
						token ^= (byte)(0xB << ((2 - ++flag) * 4)); break;
					case 'C':
					case 'c':
						token ^= (byte)(0xC << ((2 - ++flag) * 4)); break;
					case 'D':
					case 'd':
						token ^= (byte)(0xD << ((2 - ++flag) * 4)); break;
					case 'E':
					case 'e':
						token ^= (byte)(0xE << ((2 - ++flag) * 4)); break;
					case 'F':
					case 'f':
						token ^= (byte)(0xF << ((2 - ++flag) * 4)); break;
				}
				if (flag == 2)
				{
					bytes.Add(token);
					flag = 0;
					token = 0x00;
				}
			}
			while (bytes.Count < DataLength) bytes.Add(new byte());
			return bytes;
		}
	}
}
