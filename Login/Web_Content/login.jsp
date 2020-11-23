<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="ISO-8859-1">
<link rel="stylesheet" href="login.css">
<title>Game Login Page</title>
</head>
<body>
	<div class="container" id="container">
		<div class="form-container sign-up-container">
			<form action="register" method="post">
				<h1>Sign up</h1>
				<input type="text" placeholder="Username" name="Username" required />
				<input type="email" placeholder="Email" name="Email" required />
				<input type="password" placeholder="Password" name="Password" required />
				<button type="submit">Sign Up</button>
			</form>
		</div>
		<div class="form-container sign-in-container">
			<form action="login" method="post">
				<h1>Sign in</h1>
				<input type="text" placeholder="Username" name="Username" required />
				<input type="password" placeholder="Password" name="Password" required />
				<a id="forgot">Forgot your password?</a>
				<button type="submit">Sign In</button>
				<a href="guest.jsp">Or just sign me in as the guest user!</a>
			</form>
		</div>
		<div class="overlay-container">
			<div class="overlay">
				<div class="overlay-panel overlay-left">
					<h1>Game Account</h1>
					<h1>Sign In Here!</h1>
					<p>To start the game please login your account!</p>
					<button class="change" id="signIn">Sign In</button>
				</div>
				<div class="overlay-panel overlay-right">
					<h1>Game Account</h1>
					<h1>Sign Up Here!</h1>
					<p>Register your account and enjoy the game!</p>
					<button class="change" id="signUp">Sign Up</button>
				</div>
			</div>
		</div>
	</div>
</body>
<script>
	const signUpButton = document.getElementById('signUp');
	const signInButton = document.getElementById('signIn');
	const forgotButton = document.getElementById('forgot');
	const container = document.getElementById('container');
	
	signUpButton.addEventListener("click", () => {
		container.classList.add("right-panel-active");
	});
	
	forgotButton.addEventListener("click", () => {
		container.classList.add("right-panel-active");
	});
	
	signInButton.addEventListener("click", () => {
		container.classList.remove("right-panel-active");
	});
</script>
</html>