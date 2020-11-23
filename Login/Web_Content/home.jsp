<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html>
<html>
<head>
	<link rel="stylesheet" href="login.css">
	<title>User Logged In</title>
</head>
<body>
	<% String username = request.getParameter("Username"); %>
	<% String[] password = (String[]) request.getAttribute("LP"); %>
	<% int index = (int) request.getAttribute("UC"); %>
	<% String PW = password[index]; %>
	
	<h1>Welcome <% out.println(username); %>! You Have Successfully Logged in.</h1>
	<br>
	<button id="loginUser">Get Password for Game!</button>	
</body>
<script>
	const loginUserButton = document.getElementById('loginUser');

	loginUserButton.addEventListener("click", () => {
		alert("Your password is " + '<%=PW%>');
	});
</script>
</html>