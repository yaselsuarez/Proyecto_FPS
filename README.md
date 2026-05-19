# 🎮 Proyecto_FPS - Juego de Disparos en Primera Persona (Unity)

![Unity](https://img.shields.io/badge/Unity-2021.3+-000000?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Status](https://img.shields.io/badge/Estado-En%20Desarrollo-yellow?style=for-the-badge)

> 🔫 Un FPS desarrollado en Unity con sistema de combate, enemigos con IA, power-ups y minimapa.

---

## 📹 Video del Proyecto

[![Ver en YouTube](https://img.shields.io/badge/YouTube-Ver%20Video%20Completo-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://www.youtube.com/watch?v=pTZw-p_Dmug)

Mira el gameplay completo y la explicación del desarrollo en mi canal de YouTube.

---

## ✨ Características Principales

| # | Característica | Descripción |
|---|---|---|
| 01 | 🎯 **Movimiento de arma** | Ligero movimiento al girar para mayor inmersión |
| 02 | 🗺️ **Minimapa** | Minimapa funcional para orientación en el nivel |
| 03 | 🔊 **Sonidos y BSO** | Efectos de sonido y banda sonora ambiental |
| 04 | 🏃 **Sprint** | Corre con la tecla **SHIFT LEFT** |
| 05 | 🏠 **Menú Inicial** | Menú principal con opciones de juego |
| 06 | ✈️ **Teletransporte** | Puntos de teletransporte por el mapa |
| 07 | ❤️ **Power-up: Vida** | Recoge vidas para recuperar salud |
| 08 | 🔫 **Power-up: Balas** | Recoge munición extra |
| 09 | 🛡️ **Power-up: Inmunidad** | Estrellas que otorgan escudo temporal |
| 10 | 💎 **Power-up: Super Salto** | Diamantes verdes para saltar más alto |
| 11 | 👾 **IA Enemigos** | Enemigos con animaciones y comportamiento |
| 12 | 🕺 **Animaciones Jugador** | Animaciones de disparo, recarga y movimiento |
| 13 | 📊 **Barra de Vida Enemigos** | Barras de vida que siempre miran al jugador |
| 14 | 🔄 **Spawn de Enemigos** | Instancia enemigos en intervalos configurables |
| 15 | 🎁 **Spawn de Extras** | Genera power-ups de forma periódica |

---

## 🕹️ Controles

| Tecla | Acción |
|---|---|
| **WASD** | Movimiento |
| **SHIFT LEFT** | Correr |
| **SPACE** | Saltar |
| **Ratón** | Mirar / Apuntar |
| **CLICK IZQUIERDO** | Disparar |
| **R** | Recargar |
| **ESC** | Pausa |

---

## 🧠 Sistema de IA Enemiga

Los enemigos utilizan una **máquina de estados** con los siguientes comportamientos:

- **🟢 Reposo (Idle):** Estado inicial, el enemigo espera.
- **🔵 Patrulla:** El enemigo patrulla el área si el jugador no está cerca.
- **🔴 Persecución:** Detecta al jugador y lo persigue activamente.
- **⚫ Huida:** El enemigo huye a un punto seguro cuando está en desventaja.
- **💥 Ataque:** Dispara al jugador cuando está dentro del rango de ataque.

---

## ⚡ Power-Ups Disponibles

| Ícono | Tipo | Efecto |
|---|---|---|
| ❤️ | **Vida** | Recupera salud del jugador |
| 🔫 | **Bala** | Incrementa la munición disponible |
| 🛡️ | **Inmunidad** | Escudo temporal contra daño |
| 💎 | **Salto** | Super salto por tiempo limitado |
| 🏃 | **Velocidad** | Aumenta velocidad de movimiento |

---

## 🛠️ Tecnologías Utilizadas

- **Motor:** Unity 2021.3+
- **Lenguaje:** C#
- **Assets:** Flooded Grounds, Cinemachine, TextMeshPro, Post Processing
- **Patrones:** Singleton, Object Pooling (balas), Máquina de Estados (IA), Pool de Objetos

---

## 📁 Estructura del Proyecto

```
Assets/
├── Scripts/
│   ├── ControlJugador/     # Control del jugador (vida, movimiento, cámara)
│   ├── ControlEnemigos/    # IA enemiga, spawn, mundo
│   ├── OtrosScripts/       # Armas, juego, HUD, balas, extras
│   └── AI/                 # Estados de la IA (Reposo, Patrulla, Persecución, Huida, Ataque)
├── Prefabs/                # Prefabs reutilizables
├── Scenes/                 # Escenas del juego
├── Materials/              # Materiales y shaders
├── Models/                 # Modelos 3D
├── Sounds/                 # Efectos de sonido
├── music/                  # Música ambiental
├── Animations/             # Animaciones
└── Sprites/                # Sprites e iconos
```

---

## 🚀 Cómo Jugar

1. Clona el repositorio:
   ```bash
   git clone https://github.com/tuusuario/Proyecto_FPS.git
   ```
2. Abre el proyecto con **Unity 2021.3** o superior.
3. Abre la escena principal en `Assets/Scenes/`.
4. Presiona **Play** y ¡a jugar!

---

## 📌 Roadmap / Próximas Mejoras

- [ ] Más tipos de enemigos y armas
- [ ] Sistema de puntuaciones online
- [ ] Niveles adicionales
- [ ] Mejoras en la IA enemiga
- [ ] Efectos de post-procesado avanzados
- [ ] Soporte para gamepad

---

## 📝 Licencia

Este proyecto está bajo la licencia **MIT**. Si usas este código, agradecería una mención. 😊

---

<p align="center">
  Hecho con ❤️ y Unity 🎮
</p>