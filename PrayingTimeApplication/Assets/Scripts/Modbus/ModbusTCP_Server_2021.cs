using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace Modbus
{
  public class ModbusServer 
  {  	
	public UInt16[] HoldingRegister = new UInt16[1500];
	public bool[] Coil = new bool[1500];
	public Int32 port = 502;

    int ByteDataLength;
	int MessageLength;

	#region private members 	
	private TcpListener tcpListener; 
	private Thread tcpListenerThread;  		
	private TcpClient connectedTcpClient; 	
	#endregion 	

	public void StartModbusServer () 
	{ 		
		// Start TcpServer background thread 		
		tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests)); 		
		tcpListenerThread.IsBackground = true; 		
		tcpListenerThread.Start(); 	
	}

	public void ListenForIncommingRequests () 
	{ 		
		try 
		{ 			
			// Create listener on localhost port (502). 			
			tcpListener = new TcpListener(IPAddress.Any, port); 			
			tcpListener.Start();              
			Debug.Log("Server is listening");              
			Byte[] bytes = new Byte[260];  			
			while (true) 
			 { 	
                Debug.Log("Waiting for a connection... ");			
				using (connectedTcpClient = tcpListener.AcceptTcpClient()) 
				 { 					
					// Get a stream object for reading 					
					using (NetworkStream stream = connectedTcpClient.GetStream()) 
					 { 						
						int length; 						
						// Read incomming stream into byte arrary. 						
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) 
						 { 	
						    var incommingData = new byte[length]; 							
							Array.Copy(bytes, 0, incommingData, 0, length);

                            ushort Fn_code = (ushort)(bytes[7]);
							ushort Start = (ushort)(bytes[9] | (bytes[8] << 8));
                            ushort WordDataLength = (ushort)(bytes[11] | (bytes[10] << 8));	
							switch (Fn_code)
							{	
							 case 1: // Read Holding Registers (coils)
								bytes[5] = 4; //Number of bytes after this one.
								bytes[8] = 1; //Number of bytes after this one (or number of bytes of data).
								Start -= 0x800;
								bytes[9] = (byte)( Convert.ToInt32(Coil[Start]) + Convert.ToInt32(Coil[Start + 1]) * 2 + Convert.ToInt32(Coil[Start + 2]) * 4
												 + Convert.ToInt32(Coil[Start + 3]) * 8 + Convert.ToInt32(Coil[Start + 4]) * 16 + Convert.ToInt32(Coil[Start + 5]) * 32
												 + Convert.ToInt32(Coil[Start + 6]) * 64 + Convert.ToInt32(Coil[Start + 7]) * 128);
								SendMessage(bytes, 10);
							    break;

							 case 3 : // Read Holding Registers
							    ByteDataLength= WordDataLength * 2;
								bytes[5] =(byte)( ByteDataLength + 3);
								bytes[8] = (byte)ByteDataLength; //Number of bytes after this one (or number of bytes of data).
								for (int i = 0; i < WordDataLength; i++)
								{
									bytes[ 9 + i * 2] = (byte)(HoldingRegister[Start + i - 0x1000]>>8);
									bytes[10 + i * 2] = (byte)(HoldingRegister[Start + i - 0x1000] & 0xFF);
								}
								SendMessage(bytes, ByteDataLength + 9) ;
								//Debug.Log("F3");
							    break;
									
							 case 5: // Write Single Coil
							    Coil[Start - 0x800] = Convert.ToBoolean((bytes[10]) / 0XFF);
								SendMessage(bytes, 12);
								//Debug.Log("F5");
								break;

							 case 6: // Write Holding Register
								HoldingRegister[Start - 0x1000] = (UInt16)(bytes[11] | (bytes[10] << 8));
								SendMessage(bytes,12) ;  
							    break;

							 case 16: // Write Holding Registers
								ByteDataLength = WordDataLength * 2;
								bytes[5] = 6; //Number of bytes after this one. 
								for (int i = 0; i < WordDataLength; i++)
									HoldingRegister[Start + i - 0x1000] = (ushort)(bytes[ 13 + i * 2] << 8 | bytes[14 + i * 2]);
								SendMessage(bytes,12) ;
							    break;

							 case 17: // Report Slave ID
								bytes[5] = 21;
								//bytes[7] = 17;
								bytes[8] = 18; //Number of bytes after this one (or number of bytes of data).
							    bytes[9] = 200;  // 200: Application , 210: ESP
								bytes[10] = (byte)((Coil[1072]==true) ? 255 : 0) ;
								bytes[11] = 0xAA;
								bytes[12] = 0x55;
								for (int i = 13; i <27; i++)
									bytes[i] = 7;
										
								SendMessage(bytes, 18 + 9);
								//Debug.Log("F17");
								break;

								}
						 }											
					 } 					
				} 				
			 } 					
		} 		
		catch (SocketException socketException) 
		{ 			
		 Debug.Log("SocketException " + socketException.ToString()); 		
		}     
	}  	

	// Send message to client using socket connection. 	
	
	private void SendMessage(byte[] msg,int msglength) { 		
		if (connectedTcpClient == null)             
			return;         
		try { 			
			// Get a stream object for writing. 						
			NetworkStream stream = connectedTcpClient.GetStream(); 			
			if (stream.CanWrite) {                 				
				// Write byte array to socketConnection stream.               
				stream.Write(msg, 0, msglength);                             
			}       
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		} 	
	} 
  }
}