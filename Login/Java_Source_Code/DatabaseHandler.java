import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class DatabaseHandler {
	String username;
	String email;
	String password;
	
	public DatabaseHandler(String username, String email, String password) {
		this.username = username;
		this.email = email;
		this.password = password;
	}
	
	public void saveUser() throws SQLException {
		String jdbcURL = "jdbc:mysql://localhost:3306/userinformation";
		String dbUser = "root";
		String dbPassword = "root";
		
		//Class.forName("com.mysql.jdbc.Driver");
		Connection connection = DriverManager.getConnection(jdbcURL, dbUser, dbPassword);
		String sql = "INSERT INTO users (email, username, password) VALUES (?, ?, ?);";	
		
		PreparedStatement PS = connection.prepareStatement(sql);
		PS.setString(1, email);
		PS.setString(2, username);
		PS.setString(3, password);
		
		PS.executeUpdate();
		connection.close();
	}
	
	public boolean verifyUser() throws SQLException {
		String jdbcURL = "jdbc:mysql://localhost:3306/userinformation";
		String dbUser = "root";
		String dbPassword = "root";
		
		//Class.forName("com.mysql.jdbc.Driver");
		Connection connection = DriverManager.getConnection(jdbcURL, dbUser, dbPassword);
		String sql = "SELECT * FROM users WHERE username = ? and password = ?";
			
		PreparedStatement PS = connection.prepareStatement(sql);
		PS.setString(1, username);
		PS.setString(2, password);
			
		ResultSet result = PS.executeQuery();
		if (result.next()) {
			return true;
		}
		return false;
			
		//connection.close();
	}
	
}