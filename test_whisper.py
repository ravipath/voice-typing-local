import os
from pathlib import Path


# ------------------------------------------------------------
# CUDA DLL locations installed inside this virtual environment
# ------------------------------------------------------------

project_root = Path(__file__).parent

site_packages = (
    project_root
    / ".venv-whisper"
    / "Lib"
    / "site-packages"
)

cuda_dll_dirs = [
    site_packages / "nvidia" / "cublas" / "bin",
    site_packages / "nvidia" / "cudnn" / "bin",
    site_packages / "nvidia" / "cuda_nvrtc" / "bin",
]


# ------------------------------------------------------------
# Add CUDA DLL directories to PATH
# ------------------------------------------------------------

existing_path = os.environ.get("PATH", "")

for dll_dir in cuda_dll_dirs:

    if not dll_dir.exists():
        print(f"WARNING: DLL directory does not exist: {dll_dir}")
        continue

    print(f"Adding to PATH: {dll_dir}")

    os.environ["PATH"] = (
        str(dll_dir)
        + os.pathsep
        + os.environ["PATH"]
    )


# ------------------------------------------------------------
# Import faster-whisper AFTER modifying PATH
# ------------------------------------------------------------

from faster_whisper import WhisperModel


# ------------------------------------------------------------
# Audio file
# ------------------------------------------------------------

audio_file = (
    project_root
    / "bin"
    / "Debug"
    / "net10.0"
    / "recording.wav"
)


# ------------------------------------------------------------
# Load Whisper
# ------------------------------------------------------------

print()
print("Loading Whisper model...")

model = WhisperModel(
    "small.en",
    device="cuda",
    compute_type="float16",
)

print("Model loaded.")


# ------------------------------------------------------------
# Transcribe
# ------------------------------------------------------------

print()
print(f"Transcribing: {audio_file}")

segments, info = model.transcribe(
    str(audio_file),
    language="en",
)


# ------------------------------------------------------------
# Display result
# ------------------------------------------------------------

print()
print("Transcription:")
print("--------------")

for segment in segments:
    print(segment.text, end="")

print()
print()

print(f"Detected language: {info.language}")
print(f"Language probability: {info.language_probability:.3f}")