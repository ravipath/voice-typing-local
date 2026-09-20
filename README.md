# Voice Typing Tool - Local

A local Windows voice typing application under development.

The main application is built with **C# and .NET 10**. Python is currently used as an **experimental environment for testing Whisper, PyTorch, CUDA, and other machine-learning components** that may later be integrated into the application.

The goal is to build a practical voice typing tool that performs speech recognition locally, without requiring audio to be sent to a cloud service.

## Project Status

🚧 **Early development**

The project currently contains:

* Initial Windows application structure
* Audio recording experiments
* Global keyboard hook
* Python-based Whisper experiments
* PyTorch/CUDA GPU testing
* Development environment and tooling setup

The architecture and implementation will evolve as the speech-recognition pipeline is developed.

---

## Architecture

The project currently has two distinct areas.

### Windows Application

The actual desktop application is implemented in **C# / .NET 10**.

```text
┌─────────────────────┐
│   Windows Desktop   │
│     Application     │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Audio Recording   │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ Speech Recognition  │
│   (integration TBD) │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Text Processing   │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ Keyboard / Text     │
│      Injection      │
└─────────────────────┘
```

### ML / Speech Experiments

Python is currently being used separately to investigate and test machine-learning components.

```text
Python Experiments
        │
        ├── Whisper
        ├── PyTorch
        ├── CUDA / GPU
        └── Speech recognition experiments
```

These experiments are used to understand performance, GPU acceleration, model behaviour, and possible approaches for integrating speech recognition into the Windows application.

---

## Technology Stack

### Windows Application

* **C#**
* **.NET 10**
* Windows APIs
* Global keyboard hooks
* Audio capture

### ML / Experimental Environment

* **Python**
* **Whisper**
* **PyTorch**
* **NVIDIA CUDA**
* `uv` for Python environment and dependency management

### Development

* Git
* GitHub
* Visual Studio Code
* PowerShell

---

## Project Structure

```text
voice-typing/
│
├── Audio/
│   └── AudioRecorder.cs
│
├── Input/
│   └── GlobalKeyboardHook.cs
│
├── Program.cs
├── voice-typing.csproj
│
├── test_whisper.py
├── test_cuda_dll.py
│
├── install-dotnet10-sdk.ps1
│
├── README.md
├── .gitignore
│
└── ...
```

The C# source files belong to the Windows application.

The Python scripts are currently experimental/testing code and are not the primary implementation of the application.

---

## Local Speech Recognition

One of the main objectives is to use local speech recognition rather than relying on a cloud transcription service.

The project is currently evaluating **Whisper** for this purpose.

The Python environment is being used to answer questions such as:

* Which Whisper model is appropriate?
* How well does transcription perform locally?
* Can the NVIDIA GPU accelerate inference effectively?
* What are the memory and performance requirements?
* How should audio be captured and passed to the speech-recognition model?
* What is the most practical way to integrate the ML component with the Windows application?

These questions will be resolved experimentally before the final application architecture is established.

---

## GPU Acceleration

The project is also investigating GPU-accelerated speech recognition using:

* NVIDIA GPU
* CUDA
* PyTorch
* Whisper

GPU-related experiments are currently performed through Python.

This allows the ML components to be tested independently from the Windows application while the application architecture is being developed.

---

## Development Philosophy

The project is being developed incrementally.

The approach is:

```text
Windows application
        │
        ▼
Audio capture
        │
        ▼
Speech recognition experiments
        │
        ▼
Evaluate Whisper / ML options
        │
        ▼
Choose integration approach
        │
        ▼
Integrate into application
        │
        ▼
Text processing
        │
        ▼
System-wide text input
```

The Python experiments are therefore intentionally kept separate from the main application until the speech-recognition approach has been validated.

---

## Planned Features

The exact implementation is still evolving, but the intended application direction includes:

* 🎙️ Local microphone recording
* 🗣️ Local speech-to-text
* ⌨️ System-wide text input
* 🪟 Windows desktop integration
* ⚡ GPU acceleration where practical
* 🔒 Local/private processing
* ⚙️ Configurable speech-recognition models
* 📋 Text cleanup and processing
* 🚀 Fast voice-to-text workflow

---

## Development Roadmap

### Phase 1 — Foundation

* [x] Create .NET application
* [x] Set up Git repository
* [x] Implement initial audio recording component
* [x] Implement global keyboard hook
* [x] Set up Python ML experimentation environment
* [x] Test Whisper
* [x] Test PyTorch/CUDA

### Phase 2 — Speech Recognition

* [ ] Evaluate Whisper models
* [ ] Measure transcription performance
* [ ] Optimize GPU inference
* [ ] Establish audio format and processing pipeline
* [ ] Decide application/ML integration architecture

### Phase 3 — Application Integration

* [ ] Integrate speech recognition into the Windows application
* [ ] Implement voice-to-text workflow
* [ ] Implement text injection
* [ ] Add configurable activation mechanism
* [ ] Improve error handling

### Phase 4 — Production Quality

* [ ] Automated tests
* [ ] Logging and diagnostics
* [ ] Configuration management
* [ ] Performance optimization
* [ ] Packaging
* [ ] Windows installer / distributable application

### Phase 5 — CI/CD

* [ ] GitHub Actions CI
* [ ] Automated builds
* [ ] Automated tests
* [ ] Release packaging
* [ ] GitHub Releases

---

## Why Local?

The project is intended to provide a voice typing workflow where speech can be processed locally.

Potential advantages include:

* Privacy
* No dependency on a remote transcription API
* Offline capability
* Control over the speech-recognition model
* Ability to optimize the system for local hardware

The project will evaluate the practical trade-offs between model size, accuracy, latency, and hardware requirements.

---

## Current Focus

The immediate focus is **understanding and validating the speech-recognition component**.

The Windows application and Python ML experiments are being developed independently so that the ML approach can be tested without prematurely committing the application to a particular integration architecture.

---

## License

This project is open source and distributed under the **MIT License**.

See [`LICENSE`](LICENSE) for details.

---

## Status

This project is under active development.

The architecture, dependencies, and implementation details may change as the project progresses.
