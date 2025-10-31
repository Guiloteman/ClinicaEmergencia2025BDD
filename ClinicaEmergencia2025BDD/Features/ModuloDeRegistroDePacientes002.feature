Feature: ModuloDeRegistroDePacientes

Como enfermera
Quiero registrar pacientes
Para poder realizar el ingreso a urgencias
	o buscarlos durante un ingreso en caso de que
	el paciente aparezca en urgencia más de una vez

Background: 
	Given que la siguiente enfermera esta registradaaa:
		| Cuil              | Nombre | Apellido |
		| 23-12345678-8     | Ana    | Perez    |

@Escenario1
Scenario: Registro de paciente exitosa con obra social
	Given que no están cargados los pacientes en el sistema se emite el siguiente mensaje: "Paciente no registrado. No se puede ingresar a urgencias."
	| Cuil          |
	| 20-99999999-3 |
	When se cargan los siguientes pacientes:
	| Cuil          | Apellido | Nombre  | Calle   | Número | Localidad        | Obra Social | Número de Afiliación           |
	| 20-99999999-3 | Gonzalez | Alberto | Laprida | 1700   | S. M. de Tucuman | MedLife     | AFI_OSDE_01/10/2025_0000000001 |
	Then se muestra el siguiente mensaje: "¡Se Cargó con éxito!"

@Escenario2
Scenario: Registro de paciente exitosa sin obra social
	Given que no están cargados los pacientes en el sistema se emite el siguiente mensaje: "Paciente no registrado. No se puede ingresar a urgencias."
	| Cuil          |
	| 20-99999999-3 |
	When se cargan los siguientes pacientes sin obra social:
	| Cuil          | Apellido | Nombre  | Calle   | Número | Localidad        |
	| 20-99999999-3 | Gonzalez | Alberto | Laprida | 1700   | S. M. de Tucuman |
	Then se muestra el siguiente mensaje: "¡Se Cargó con éxito!"

@Escenario3
Scenario: Registro de paciente exitosa con obra social inexistente
	Given que no están cargados los pacientes en el sistema se emite el siguiente mensaje: "Paciente no registrado. No se puede ingresar a urgencias."
	| Cuil          |
	| 20-99999999-3 |
	When se cargan los siguientes pacientes:
	| Cuil          | Apellido | Nombre  | Calle         | Número | Localidad        | Obra Social   | Número de Afiliación |
	| 20-99999999-3 | Gonzalez | Alberto | Laprida       | 1700   | S. M. de Tucuman | Carmelo Salud | 1234567899           |
	Then se muestra el siguiente mensaje de error: "¡No se puede registrar al paciente con una obra social inexistente!"

@Escenario4
Scenario: Registro de paciente exitosa con obra social existente pero no esta afiliado
	Given que no están cargados los pacientes en el sistema se emite el siguiente mensaje: "Paciente no registrado. No se puede ingresar a urgencias."
	| Cuil          |
	| 20-99999999-3 |
	When se cargan los siguientes pacientes:
	| Cuil          | Apellido | Nombre  | Calle   | Número | Localidad        | Obra Social | Número de Afiliación |
	| 20-99999999-3 | Gonzalez | Alberto | Laprida | 1700   | S. M. de Tucuman | MedLife     | 123456789            |
	Then se muestra el siguiente mensaje de error: "¡No se puede registrar al paciente dado que no está afiliado a la obra social!"
