import java.io.IOException;
import java.sql.SQLException;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
 
@WebServlet("/login")
public class login extends HttpServlet {
	private static final long serialVersionUID = 1L;
	private String[] loginPassword = new String[]{"HH2PN", "JK982", "LC79K", "TB2P1", "GQ52P"};
	private String[] guestPassword = new String[] {"MCA82", "ASC34", "ASD8S", "PASD1", "ERT65"};
	private static int userCount = 0;
	private static int guestCount = 0;

	public login() {
        super();
    }
 
	protected void doPost(HttpServletRequest request, HttpServletResponse response) 
			  throws ServletException, IOException {
		String username = request.getParameter("Username");
		String password = request.getParameter("Password");
		
		if(username.isEmpty() || password.isEmpty() ) {
			request.getRequestDispatcher("login.jsp").include(request, response);
		}
		else {
			DatabaseHandler dbh = new DatabaseHandler(username, "" ,password);
			boolean verify = true;
			try {
				verify = dbh.verifyUser();
			} catch (SQLException e) {
				e.printStackTrace();
			}
			
			if (!verify) {
				String message = "Wrong Username/Password";
                request.setAttribute("message", message);
				request.getRequestDispatcher("login.jsp").include(request, response);
			}
			else {
				request.setAttribute("LP", loginPassword);
				request.setAttribute("UC", (userCount % 5));
				userCount++;
				request.getRequestDispatcher("home.jsp").forward(request, response);
			}
		}
	}
}