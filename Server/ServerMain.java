

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;


public class ServerMain{
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		int port;
		System.out.println("Input port number");
		port = scan.nextLine();

		
		new Server(port);
	}


	private static ArrayList<ClientHandler> clients = new ArrayList<>();
	private ExecutorService pool = Executors.newFixedThreadPool(6);

	public Server(int port)
	 {
		outputstreams = new ArrayList<ObjectOutputStream>();
		inputstreams = new ArrayList<BufferedReader>();
		try {
			ServerSocket ss = new ServerSocket(port);
			System.out.println("Server started at port " + port );
			int numplayers = 0;
			while(true) {
				Socket socket = ss.accept();
				System.out.println("Connection accepted from" + socket.getInetAddress());
				ClientHandler clientThread = new ClientHandler(socket);
				clients.add(clientThread);
				updateClients(clientThread);
				pool.execute(clientThread);
				numplayers++;
	            if(numplayers == 5) {	
	            	break;
	            }
			}
          
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	public void updateClients(ClientHandler handler) {
		for(int i=0; i<clients.size(); i++) {
			clients.get(i).add(handler);
		}
	}
	
}