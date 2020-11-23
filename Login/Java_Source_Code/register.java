import java.io.IOException;
import java.sql.SQLException;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
 
@WebServlet("/register")
public class register extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    protected void doPost(HttpServletRequest request, HttpServletResponse response) 
    		  throws ServletException, IOException {
		String username = request.getParameter("Username");
		String email = request.getParameter("Email");
		String password = request.getParameter("Password");
		
		if(username.isEmpty() || email.isEmpty() || password.isEmpty()) {
			request.getRequestDispatcher("login.jsp").include(request, response);
		}
		else {
			DatabaseHandler dbh = new DatabaseHandler(username, email, password);
			try {
				dbh.saveUser();
			} catch (SQLException e) {
				e.printStackTrace();
			}
			
			request.getRequestDispatcher("login.jsp").forward(request, response);
		}
	}
 
}