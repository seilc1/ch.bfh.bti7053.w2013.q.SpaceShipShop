<form class="login-form">
	<div>
		<label for="loginname">Loginname</label>
		<input type="text" id="loginname" data-bind="value: loginname"/>
	</div>
	<div>
		<label for="password">Password</label>
		<input type="password" id="password" data-bind="value: password"/>
	</div>
	<div class="button-box">
		<button class="btn" data-bind="click: authenticate">Login</button>
	</div>
</form>