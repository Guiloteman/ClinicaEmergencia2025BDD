Feature: ModuloDeRegistroDePacientes

Como enfermera
Quiero registrar pacientes
Para poder realizar el ingreso a urgencias
	o buscarlos durante un ingreso en caso de que
	el paciente aparezca en urgencia más de una vez

Background: 
	Given que la siguiente enfermera esta registrada:
		| Nombre | Apellido |
		| Ana    | Perez    |

@Escenario1
Scenario: Registro de paciente exitosa
	Given que no están cargados los pacientes en el sistema se emite el siguiente mensaje: "Paciente no registrado. No se puede ingresar a urgencias."
	| Cuil          |
	| 20-99999999-3 |
	When se cargan los siguientes pacientes:
	| Cuil          | Apellido | Nombre  | Calle   | Número | Localidad        | Obra Social | Número de Afiliación |
	| 20-99999999-3 | Gonzalez | Alberto | Laprida | 1700   | S. M. de Tucuman | MedLife     | AFI-21/10/2025-20000 |
	Then se muestra el siguiente mensaje: "¡Se Cargó con éxito!"
