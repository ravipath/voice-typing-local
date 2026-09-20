import ctypes
from pathlib import Path


root = Path(__file__).parent / ".venv-whisper" / "Lib" / "site-packages"

dll_dirs = [
    root / "nvidia" / "cublas" / "bin",
    root / "nvidia" / "cudnn" / "bin",
    root / "nvidia" / "cuda_nvrtc" / "bin",
]

for directory in dll_dirs:
    print(f"Adding DLL directory: {directory}")
    ctypes.windll.kernel32.SetDllDirectoryW(str(directory))


cublas = root / "nvidia" / "cublas" / "bin" / "cublas64_12.dll"

print()
print(f"Testing: {cublas}")
print(f"Exists:  {cublas.exists()}")
print()

try:
    dll = ctypes.CDLL(str(cublas))
    print("SUCCESS: cublas64_12.dll loaded!")
except Exception as e:
    print("FAILED to load cublas64_12.dll")
    print()
    print(e)