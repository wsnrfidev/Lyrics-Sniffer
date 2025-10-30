<h1 align="center">🎧 CLI Lyrics Sniffer</h1>
<p align="center">
  <b>A minimalist Spotify-connected command-line lyrics visualizer — built with ❤️ using C# and .NET 8</b><br>
  Designed for developers who love coding while listening to music — with real-time synced lyrics right in your terminal.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-blueviolet?style=for-the-badge&logo=dotnet" />
  <img src="https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20Linux-0078D4?style=for-the-badge" />
  <img src="https://img.shields.io/badge/License-MIT-green?style=for-the-badge" />
</p>

---

## 🧠 Overview

**CLI-Lyrics-Sniffer** connects directly to your **Spotify account**, detects the currently playing song,  
and displays synchronized lyrics fetched from [lrclib.net](https://lrclib.net).  
All lyrics are rendered beautifully and **centered inside your terminal** — just like karaoke, but for developers 🎵.

---

## 🚀 Features

| Feature | Description |
|----------|-------------|
| 🎵 **Spotify Integration** | Detects currently playing track via Spotify Web API |
| ⏱ **Live Synced Lyrics** | Fetches and syncs with real-time song progress |
| 🧠 **Smart Auto-Resync** | Detects desyncs when Spotify seek/lag happens |
| 💡 **Minimalist Center Layout** | Clean & centered lyrics with highlight animation |
| 🪄 **Smart Run System** | Auto-detects project path, just type `lyrics-sniffer` |
| ⚙️ **Cross-Platform Ready** | Runs perfectly on Windows, macOS, and Linux |
| 🧩 **Clean Architecture** | Fully refactored modular structure — easy to maintain and extend |

---

## 🧩 Tech Stack

- **Language:** C#
- **Framework:** .NET 8.0  
- **API:** Spotify Web API + [LrcLib API](https://lrclib.net)  
- **Rendering:** ANSI Console + Custom Renderer  
- **Architecture:** Modular MVC-like with Core Services  

---

## 📁 Folder Structure
```
CLI-Lyrics-Sniffer/
│
├── Core/
│ ├── Models/
│ │ ├── PlayingInfo.cs
│ │ ├── LyricsData.cs
│ │ └── LrcLine.cs
│ │
│ ├── Services/
│ │ ├── SpotifyAuth.cs
│ │ ├── SpotifyService.cs
│ │ └── LyricsService.cs
│ │
│ ├── Render/
│ │ └── KaraokeRenderer.cs
│ │
│ └── Utils/
│ ├── ConsoleHelper.cs
│ ├── PathHelper.cs
│ └── Theme.cs
│
├── appsettings.json
├── CLI-Lyrics-Sniffer.csproj
├── Program.cs
└── README.md
```


---

## 🧰 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Spotify account
- Internet connection

---

## ⚙️ Setup Guide

### 🪄 1️⃣ Clone the Repository
```bash
git clone https://github.com/wsnrfidev/Lyrics-Sniffer
cd Lyrics-Sniffer
```

### 🔐 2️⃣ Add Your Spotify Credentials

Create appsettings.json in the project root with:
```
{
  "Spotify": {
    "ClientId": "YOUR_SPOTIFY_CLIENT_ID",
    "RedirectUri": "http://localhost:5000/callback"
  }
}
```

# To get your credentials:
```
1. Go to Spotify Developer Dashboard

2. Create a new app

3. Copy your Client ID

3. Add Redirect URI → http://localhost:5000/callback

4. Click Save Changes
```

### 🏃 How to Run

## 🅰️ From Source
```bash
dotnet run --project ./CLI-Lyrics-Sniffer/CLI-Lyrics-Sniffer.csproj
```

## 🅱️ Global Tool (Smart Run)

# Make it globally accessible:
```bash
dotnet pack -c Release
dotnet tool install --global --add-source "./CLI-Lyrics-Sniffer/nupkg" CliLyricsSniffer
```

# Then simply type:
```
lyrics-sniffer
```

🎉 It will auto-detect your project root and run immediately — no cd required!

### 🖥 Example Output
```
🎵 Perfect — Ed Sheeran
===========================================

⏱️ 04:23 | Offset 01:02
│─────────────────────────────────────────│
▶ PLAYING

                • I found a love for me •
                  Darling just dive right in
                   Follow my lead
```
### 💬 Common Issues
❌ Couldn't find a project to run

# Try:
```bash
lyrics-sniffer
```

or specify project manually:
```bash
dotnet run --project ./CLI-Lyrics-Sniffer/CLI-Lyrics-Sniffer.csproj
```
# ⚠️ Lyrics Not Found

Some songs may not have synced lyrics in LrcLib — check manually at lrclib.net
.

# 💤 No music playing

Make sure Spotify Desktop app is running and you're logged into the same account.   

🧱 Clean Code Practices

✅ Modular structure under /Core/
✅ Separation of concerns (Models, Services, Utils)
✅ Async/await for non-blocking network I/O
✅ ANSI-safe console output
✅ Works on all major terminals

🧩 Future Roadmap (v2)

🎨 Animated lyric transitions

🔁 Real-time re-sync with Spotify seek

🎵 Support for Apple Music & YouTube Music

🌈 Dynamic color themes

💾 Offline lyric caching

🧰 One-click installer wizard

📦 Build & Package
dotnet clean
dotnet build -c Release
dotnet pack -c Release


# Output:
```
/CLI-Lyrics-Sniffer/nupkg/CliLyricsSniffer.1.0.0.nupkg
```

### 🧹 Recommended .gitignore
```
bin/
obj/
.vs/
*.user
*.suo
appsettings.Development.json
appsettings.Local.json
.DS_Store
Thumbs.db
```

### 📜 License
```
MIT License © 2025 [Wisnu Rafi]
Made with ❤️ by Wisnu Rafi & ChatGPT 🤖
```

### ⭐ Credits
```
Spotify Web API

LrcLib.net

Microsoft .NET

Newtonsoft.Json
```
<p align="center">⭐ Star the repo if you love it! Let's make coding with music better 🎶💻</p>
