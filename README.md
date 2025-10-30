# 🎧 CLI Lyrics Sniffer

> A minimalist Spotify-connected **command-line lyrics visualizer**, built with ❤️ using **C#** and **.NET 8**.  
> Designed for developers who love coding while listening to music — with real-time synced lyrics right in your terminal.

---

## 🧠 Overview

**CLI-Lyrics-Sniffer** connects directly to your **Spotify account**, detects the current playing song, and displays synchronized lyrics fetched from [lrclib.net](https://lrclib.net).  
All lyrics are rendered beautifully and centered inside your terminal — just like karaoke, but in code form.

---

## 🚀 Features

| Feature | Description |
|----------|-------------|
| 🎵 **Spotify Integration** | Detects currently playing track via Spotify Web API |
| ⏱ **Live Synced Lyrics** | Fetches and syncs with real-time song progress |
| 🧠 **Smart Auto-Resync** | Detects desyncs when Spotify seek/lag happens |
| 💡 **Minimalist Center Layout** | Clean & centered lyrics with highlight animation |
| 🪄 **Smart Run System** | Auto-detects project path, just type `lyrics-sniffer` |
| ⚙️ **Cross-Platform Ready** | Built on .NET 8 for Windows, macOS, and Linux |
| 🧩 **Clean Architecture** | Fully refactored code — easy to maintain and extend |

---

## 🧩 Tech Stack

- **Language:** C#
- **Framework:** .NET 8.0
- **API:** Spotify Web API + [LrcLib API](https://lrclib.net)
- **Rendering:** ANSI Console + Custom Renderer
- **Architecture:** Modular MVC-like with Core Services

---

## 📁 Folder Structure

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

---

## 🧰 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Spotify account
- Internet connection

---

## ⚙️ Setup Guide

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/<your-username>/CLI-Lyrics-Sniffer.git
cd CLI-Lyrics-Sniffer

### 2️⃣ Add Your Spotify Credentials
{
  "Spotify": {
    "ClientId": "YOUR_SPOTIFY_CLIENT_ID",
    "RedirectUri": "http://localhost:5000/callback"
  }
}

# To get your credentials:

Go to Spotify Developer Dashboard

Create a new app

Copy Client ID

Add Redirect URI: http://localhost:5000/callback

Save changes

### 🏃 Run the Program
### 🅰️ Run from source
dotnet run --project ./CLI-Lyrics-Sniffer/CLI-Lyrics-Sniffer.csproj

### 🅱️ Global Tool (Smart Run)

Make it available everywhere using:

dotnet pack -c Release
dotnet tool install --global --add-source "./CLI-Lyrics-Sniffer/nupkg" CliLyricsSniffer


Now you can simply type:

lyrics-sniffer


and it will auto-detect the project root and run directly.
