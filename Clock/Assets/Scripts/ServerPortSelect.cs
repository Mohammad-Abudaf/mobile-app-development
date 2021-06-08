using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace ServerPortSelect{

	public class ModbusServer{

		public ushort[] HoldingRegister = new ushort[1500];
		public bool[] Coil = new bool[1500];
		public int port;

		private int ByteDataLength;
		private int MessageLength;

		#region private members
		private TcpListener tcpListener;
		private Thread tcpListenerThread;
		private TcpClient connectedTcpClient;
		#endregion

		public ModbusServer (){
			// Start TcpServer background thread
			tcpListenerThread = new Thread (new ThreadStart(ListenForIncomingRequests));
			tcpListenerThread.IsBackground = true;
			tcpListenerThread.Start();
			HoldingRegister[1313] = 0;
			HoldingRegister[1314] =0;
			HoldingRegister[1315] = 0;
			Coil[1072] = false;
			port = 502;
		}

		private void ListenForIncomingRequests ()
		{
			try{
				// Create listener on localhost port (502).
				tcpListener = new TcpListener(IPAddress.Any, port);
				tcpListener.Start();
				Debug.Log("Server is listening");
				var bytes = new byte[260];
				while (true) {
					Debug.Log("Waiting for a connection... ");
					using (connectedTcpClient = tcpListener.AcceptTcpClient())
					{
						// Get a stream object for reading
						using (var stream = connectedTcpClient.GetStream()) {
							int length;
							// Read incoming stream into byte array.
							while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
							{
								var incomingData = new byte[length];
								Array.Copy(bytes, 0, incomingData, 0, length);

								var Fn_code = (ushort)(bytes[7]);
								var Start = (ushort)(bytes[9] | (bytes[8] << 8));
								var WordDataLength = (ushort)(bytes[11] | (bytes[10] << 8));
								switch (Fn_code)
								{
									// 03 Read Holding Registers
									case 3 :
										ByteDataLength= WordDataLength * 2;
										bytes[5] =(byte)( ByteDataLength + 3);
										bytes[8] = (byte)ByteDataLength; //Number of bytes after this one (or number of bytes of data).
										for (var i = 0; i < WordDataLength; i++)
										{
											bytes[ 9 + i * 2] = (byte)(HoldingRegister[Start + i - 0x1000]>>8);
											bytes[10 + i * 2] = (byte)(HoldingRegister[Start + i - 0x1000] & 0xFF);
										}
										MessageLength = ByteDataLength + 9;
										SendMessage(bytes,MessageLength) ;
										break;
									// 06 Write Holding Register
									case 6:
										HoldingRegister[Start - 0x1000] = (UInt16)(bytes[11] | (bytes[10] << 8));
										bytes[5] = 6; //Number of bytes after this one.
										MessageLength = 12;
										SendMessage(bytes,MessageLength) ;
										break;
									// 01 Read Holding Registers (coils)
									case 1:
										bytes[9]=0;
										ByteDataLength = 1 ;
										bytes[5] = (byte) (ByteDataLength + 3); //Number of bytes after this one.
										bytes[8] =(byte) ByteDataLength; //Number of bytes after this one (or number of bytes of data).
										for (int i = Start; i < (WordDataLength+Start); i++)
										{
											bytes[9]+=(byte)(Math.Pow(2, (i-Start))* Convert.ToInt32(Coil[i])); // revice ????????
										}
										MessageLength = ByteDataLength + 9;
										SendMessage(bytes,MessageLength) ;
										break;
									// 05 Write Single Coil
									case 5:
										Coil[Start - 0x800] =Convert.ToBoolean((bytes[10])/0XFF);
										bytes[5] = 6; //Number of bytes after this one.
										MessageLength = 12;
										SendMessage(bytes,MessageLength) ;
										break;
									//16 Write Holding Registers
									case 16:
										ByteDataLength = WordDataLength * 2;
										bytes[5] = (byte)(ByteDataLength + 3); //Number of bytes after this one.
										for (int i = 0; i < WordDataLength; i++)
											HoldingRegister[Start + i - 0x1000] = (ushort)(bytes[ 13 + i * 2] << 8 | bytes[14 + i * 2]);
										MessageLength = 12;
										SendMessage(bytes,MessageLength) ;
										break;
								}
							}
						}
					}
				}

			}
			catch (SocketException socketException)
			{
				Debug.Log($"SocketException {socketException}");
			}
		}

		// Send message to client using socket connection.

		private void SendMessage(byte[] msg,int msgLength) {
			if (connectedTcpClient == null)
				return;
			try {
				// Get a stream object for writing.
				var stream = connectedTcpClient.GetStream();
				if (stream.CanWrite) {
					// Write byte array to socketConnection stream.
					stream.Write(msg, 0, msgLength);
				}
			}
			catch (SocketException socketException) {
				Debug.Log($"Socket exception: {socketException}");
			}
		}
	}

}
