Feature: Modulo de Urgencias

Como enferera
Quiero porder registrar las admisiones de los pacientes a emergencias
Para determinar que pacientes tienen mayor prioridad de atención

Background: 
	Given que la siguiente enfermera esta registrada:
		| Nombre | Apellido |
		| Ana    | Perez    |

@tag1
Scenario: Ingreso de paciente a la lista de espera de emergencias
	Given que estan registrados los siguientes pacientes:
		| Cuil          | Nombre | Apellido | Obra Social |
		| 20-11111111-3 | Juan   | Gomez    | Galeno      |
		| 27-22222222-6 | Maria  | Lopez    | MedLife     |
		| 20-33333333-8 | Carlos | Sanchez  | OSDE        |
	When ingresan a urgencias los siguientes pacientes:
		| Cuil          | Informe                  | Nivel de Emergencia | Temperatura | Frecuencia Cardíaca | Frecuencia Respiratoria | Tensión Arterial |
		| 20-11111111-3 | Dolor de cabeza severo   | 3                   | 37.5        | 80                  | 18                      | 120/80           |
		| 27-22222222-6 | Fractura en brazo        | 2                   | 36.8        | 75                  | 16                      | 118/76           |
		| 20-33333333-8 | Dificultad para respirar | 1                   | 38.2        | 95                  | 22                      | 130/85           |
	Then los pacientes deben ser añadidos a la cola de atencion ordenados por cuil de la siguiente manera:
		| Cuil          |
		| 27-22222222-6 |
		| 20-11111111-3 |
		| 20-33333333-8 |

@tag2
Scenario: Ingreso de paciente, no registrado, a la lista de espera de emergencias
	Given que estan registrados los siguientes pacientes:
		| Cuil          | Nombre | Apellido | Obra Social |
		| 20-11111111-3 | Juan   | Gomez    | Galeno      |
		| 27-22222222-6 | Maria  | Lopez    | MedLife     |
		| 20-33333333-8 | Carlos | Sanchez  | OSDE        |
	When ingresan a urgencias los siguientes pacientes:
		| Cuil          | Informe                  | Nivel de Emergencia | Temperatura | Frecuencia Cardíaca | Frecuencia Respiratoria | Tensión Arterial |
		| 20-44444444-3 | Dolor de cabeza severo   | 3                   | 37.5        | 80                  | 18                      | 120/80           |
	Then se muestra el mensaje de error "Paciente no registrado. No se puede ingresar a urgencias."
		| Cuil          |
		| 20-44444444-3 |

@tag3
Scenario: Ingreso de paciente, pero algunos de los datos fue omitido
	Given que estan registrados los siguientes pacientes:
		| Cuil          | Nombre | Apellido | Obra Social |
		| 20-11111111-3 | Juan   | Gomez    | Galeno      |
		| 27-22222222-6 | Maria  | Lopez    | MedLife     |
		| 20-33333333-8 | Carlos | Sanchez  | OSDE        |
	When ingresan a urgencias los siguientes pacientes:
		| Cuil          | Informe                  | Nivel de Emergencia | Temperatura | Frecuencia Cardíaca | Frecuencia Respiratoria | Tensión Arterial |
		| 20-33333333-8 | Dolor de cabeza severo   |                     | 37.5        | 80                  | 18                      | 120/80           |
	Then se muestra el mensaje de error "Faltan agregar algunos datos.".
		| Cuil          | Informe                  | Nivel de Emergencia | Temperatura | Frecuencia Cardíaca | Frecuencia Respiratoria | Tensión Arterial |
		| 20-33333333-8 | Dolor de cabeza severo   |                     | 37.5        | 80                  | 18                      | 120/80           |
