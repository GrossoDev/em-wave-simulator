# Simulador de interferencia de ondas

Este repositorio contiene un simulador de interferencia de ondas generadas por varias fuentes mediante el principio de superposición.

## Marco teórico

### Fuente generadora de ondas

La intensidad en un punto, inducido por una fuente generadora de ondas, se calcula como:

$$s(\vec{r}, t) = A\cdot \text{sen}(kx-\omega t + \varphi)$$

Donde:
 - $\vec{r}$ es la posición en el plano.
 - $t$ es un instante de tiempo.
 - $A$ es la amplitud de la onda.
 - $\varphi$ es la fase de la onda.
 - $k$ es el número de onda.
 - $\omega$ es la velocidad angular.

Estos dos últimos valores, $k$ y $\omega$ dependen de la frecuencia de la onda:
 - $\omega = 2\pi f$, siendo $f$ la frecuencia de la onda.
 - $k = 2\pi / \lambda$, siendo $\lambda$ la longitud de la onda.
 - $\lambda = c / f$, siendo $c$ la velocidad de propagación de la onda en el medio y $f$ la frecuencia.

### Ley inversa del cuadrado

La ley inversa del cuadrado establece que la intensidad de los fenómenos ondulatorios decae de manera inversamente proporsional al cuadrado de la distancia entre la fuente y el punto del espacio.

Entonces, de la fórmula anterior:

$$s(\vec{r}, t) = \frac{A\cdot \text{sen}(kx-\omega t + \varphi)}{r^2}$$

Donde $r$ es la distancia entre la fuente y el punto $\vec{r}$.

### Principio de superposición

El principio de superposición indica que si existen $n$ fuentes generadoras de ondas, entonces el desplazamiento inducido sobre un punto es igual a la suma del desplazamiento que produciría cada una por si sola:

$$s_{total}(\vec{r}, t)=\sum_i^n s_i(\vec{r}, t)$$

## Código clave del proyecto

En el siguiente archivo se encuentra el código central del proyecto:

 - [FieldVisualizer.shader](src/Unity/Assets/Coordinator/Simulation/Wave/FieldVisualizer.shader): Es un shader de fragmento de DirectX que calcula la intensidad para punto en el espacio que corresponda a un pixel en la pantalla.

## Características
 - **Cantidad arbitraria de fuentes**: Se pueden visualizar el patrón de interferencia generado por cualquier cantidad de fuentes (máximo 50).
 - **Valores arbitrarios para cada fuente**: Cada fuente tiene sus propios parámetros de amplitud, frecuencia y fase.
 - **Multiples fuentes como matriz en fase**: Se pueden seleccionar varias fuentes para que se comporten como una matriz en fase, ajustando su separación según su longitud de onda y agregando un desfasaje a cada una.
 - **Simulación a escala**: La simulación está preparada para mostrar valores a escala real con unidades SI, para su uso cuantitativo.
 - **Simulación en tiempo real**: Construida con Unity, la simulación está basada en shaders de fragmento, por lo que aprovecha la GPU para realizar el cálculo de manera altamente paralela.

## Objetivos del Proyecto

Este proyecto cumple con los requerimientos del trabajo final de promoción de electromagnetismo: _**Trabajo 2**: Interferencia de Ondas_, que incluye:

 - Solicitar al usuario las características de cada onda (amplitud, frecuencia, fase).
 - Sumar las ondas y graficar la onda resultante.
 - Mostrar los casos de interferencia constructiva y destructiva en diferentes puntos de la onda.

Conceptos Cubiertos:
 - Interferencia constructiva.
 - Interferencia destructiva.
 - Superposición de ondas.

Tecnologías Utilizadas
 - Unity: Motor de juegos utilizado la simulación.
 - WebGL: Motor de renderizado para la web.
 - HLSL: Lenguaje para la escritura del shader.
 - C#: Lenguaje para el desarrollo de los scripts del proyecto.

## Acceder online

Se puede acceder a la calculadora en [https://grossodev.github.io/em-wave-simulator](https://grossodev.github.io/em-wave-simulator)

## Uso
### Comandos generales
 - **Tecla P**: abre un cuadro para cambiar los parámetros de la simulación.
 - **Tecla ESCAPE**: cierra todas las ventanas y deselecciona todas las fuentes.
### Manejo de la cámara
 - **Rueda del mouse**: acerca o aleja el zoom.
 - **Click de la rueda del mouse + arrastre del mouse**: mueve la cámara.
 - **Tecla M + arrastre del mouse**: mueve la cámara (lo mismo que la anterior).
### Manejo de las fuentes
 - **Click derecho en un punto del espacio**: crea una nueva fuente.
 - **Click izquierdo en una fuente**: selecciona o deselecciona una fuente.
 - **Click izquierdo en una fuente + arrastrar**: mueve la fuente, o las fuentes si hay mas de una seleccionada.
 - **Click izquierdo en un punto del espacio + arrastrar**: crea un cuadro de selección.
 - **Tecla I**: abre un cuadro para cambiar los parámetros de la(s) fuente(s) seleccionada(s).
 - **Tecla SUPRIMIR**: elimina la(s) fuente(s) seleccionada(s).

## Autor
Giuliano Rosso, 2024
