Feature: ModuloDeAutenticacion-005

Como usuario del sistema
Quiero poder registrarme
Para poder acceder a las actividades que ne son otorgadas

@tag1
Scenario: Iniciar Sesion con usuario y contraseña válidos
	Given existen los siguientes usuarios:
	| Email                   | Contraseña | Autoridad |
	| emi_enfermera@gmail.com |       1234 | enfermera |
	When ingreso los siguientes datos para acceder al sistema:
	| Email                   | Contraseña | Autoridad |
	| emi_enfermera@gmail.com |       1234 | enfermera |
	Then puedo ver y ejecutar las historias correspondiente

@tag2
Scenario: Iniciar Sesion con usuario y contraseña inválidos
	Given existen los siguientes usuarios:
	| Email                   | Contraseña | Autoridad |
	| emi_enfermera@gmail.com |       1234 | enfermera |
	When ingreso los siguientes datos para acceder al sistema:
	| Email                   | Contraseña | Autoridad |
	| ana_enfermera@gmail.com |       5678 | enfermera |
	Then se emite el siguiente mensaje: "Usuario y Contraseña inválidos."

