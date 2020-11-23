
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;

public class ClientHandler implements Runnable{

private Socket client;
private ObjectOutputStream out;
private ObjectInputStream in;
private ArrayList<ClientHandler> allClients = new ArrayList<>();

	public class ClientHandler(Socket socket) throws IOException {
		this.client = socket;
		in = new ObjectInputStream(client.getInputStream());
		out = new ObjectOutputStream(client.getOutputStream());
	}

	public void add(ClientHandler handler) {
		allClients.add(handler);
	}

	public void run() {
		try {

		}
		finally {
			out.close();
			in.close();
		}

	}

}