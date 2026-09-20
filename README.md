# 🎙️ Voice Typing Tool — Local

A **local voice-to-text typing tool for Windows**, designed to turn spoken words into text directly on the desktop.

The goal is simple:

> **Speak naturally → get text wherever you are typing.**

The application uses local speech recognition so that audio processing can be performed on the user's machine, without requiring a cloud transcription service.

---

## ✨ Features

* 🎤 Record speech from the microphone
* 📝 Convert speech to text
* 💻 Local speech recognition
* ⚡ Fast transcription using GPU acceleration when available
* 🔒 No dependency on cloud speech APIs
* 🪟 Designed for Windows desktop usage
* 🧩 Modular architecture for future extensions

### Planned

* [ ] Global keyboard shortcut to start/stop recording
* [ ] Type transcription into the currently focused application
* [ ] Push-to-talk mode
* [ ] Automatic punctuation
* [ ] Configurable transcription models
* [ ] Voice activity detection
* [ ] Transcription history
* [ ] System tray integration
* [ ] Configuration file / settings
* [ ] Automated tests
* [ ] CI pipeline
* [ ] CD / release pipeline
* [ ] Packaged Windows executable

---

## 🏗️ Architecture

The project is intentionally being developed as a small, modular local application.

```text
┌─────────────────────┐
│      Microphone     │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Audio Capture     │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ Speech Recognition  │
│      (Whisper)      │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│  Text Processing    │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Keyboard Output   │
└─────────────────────┘
```

The architecture will evolve as the application gains more functionality.

---

## 🧠 Technology

Current technology stack:

| Component             | Technology  |
| --------------------- | ----------- |
| Language              | Python      |
| Environment           | `uv`        |
| Speech recognition    | Whisper     |
| ML framework          | PyTorch     |
| Hardware acceleration | NVIDIA CUDA |
| Platform              | Windows     |
| Version control       | Git         |

The project uses separate environments where appropriate to keep the development environment and ML dependencies manageable.

---

## 🚀 Getting Started

### Prerequisites

You will need:

* Windows
* Python
* [`uv`](https://docs.astral.sh/uv/)
* Git
* NVIDIA GPU + CUDA support *(optional, but recommended for accelerated transcription)*

### Clone the repository

```bash
git clone <repository-url>
cd voice-typing
```

### Create the environment

```bash
uv sync
```

### Run the application

```bash
uv run python app.py
```

> The exact application entry point may change while the project is under development.

---

## 🧪 Development

The project is being developed incrementally, with experiments and implementation kept separate where practical.

Typical workflow:

```bash
uv sync
uv run python <script>
```

Run tests with:

```bash
uv run pytest
```

As the project matures, formatting, linting, testing, and type checking will become part of the automated development workflow.

---

## 🔄 CI/CD

CI/CD is planned as part of the project rather than being added as an afterthought.

The intended pipeline will eventually cover:

```text
Git Push
   │
   ▼
┌───────────────┐
│   Lint / Type │
│    Checks     │
└───────┬───────┘
        │
        ▼
┌───────────────┐
│     Tests     │
└───────┬───────┘
        │
        ▼
┌───────────────┐
│ Build / Package│
└───────┬───────┘
        │
        ▼
┌───────────────┐
│    Release    │
└───────────────┘
```

The initial CI pipeline will focus on:

* dependency installation
* code quality checks
* automated tests
* build verification

CD will later be extended to produce distributable Windows releases.

---

## 📁 Project Structure

The structure will evolve as implementation progresses.

```text
voice-typing/
│
├── app.py
├── pyproject.toml
├── uv.lock
├── README.md
├── .gitignore
│
├── src/
│   └── ...
│
├── tests/
│   └── ...
│
└── .github/
    └── workflows/
        └── ...
```

---

## 🎯 Project Goals

This project is also an exercise in building a **complete AI-powered software application**, not just experimenting with a speech-recognition model.

The long-term focus is therefore on:

* clean software architecture
* local AI inference
* hardware acceleration
* reproducible environments
* automated testing
* CI/CD
* packaging and deployment
* maintainability

---

## 📌 Status

**🚧 Early development**

The project is currently being built incrementally. APIs, architecture, and implementation details may change.

---

## 📄 License

License to be decided.
