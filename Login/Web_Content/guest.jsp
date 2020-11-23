<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html>
<html>
<head>
	<link rel="stylesheet" href="login.css">
	<title>Guest Logged In</title>
</head>
<body>
	<h1>Welcome Guest Player!</h1>
	<br>
	<button id="guestUser">Get Password for Game!</button>
</body>
<script>
	var passwordArray = ['MCA82', 'ASC34', 'ASD8S', 'PASD1'];
	const guestUserButton = document.getElementById('guestUser');
	const randomPassword = passwordArray[Math.floor(Math.random() * passwordArray.length)];
	
	guestUserButton.addEventListener("click", () => {
		alert("Your password is " + randomPassword);
	});
</script>
</html>