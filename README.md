# Obviousidian

**Obviousidian** is a lightweight, pragmatic Windows desktop application designed for rapid content capture into an [Obsidian](https://obsidian.md/) vault. 

It runs independently of Obsidian, allowing you to capture ideas, links, and images without opening the main application, minimizing friction and context switching.

## ✨ Features

- **Blazing Fast Capture**: specialized for quick input.
- **Standalone Operation**: Works whether Obsidian is open, closed, or loading.
- **Smart Routing**: Automatically detects content type and saves it to the appropriate folder:
    - **Text** → `inbox/`
    - **Images** (Clipboard) → `attachments/` (with a linked note in `screenshots/`)
    - **URLs** → `bookmarks/` or `videos/` (if YouTube/Vimeo)
- **Direct Filesystem Access**: Writes standard Markdown (`.md`) files directly to your vault.
- **Native Performance**: Built with C# / WPF and .NET 8 for a tiny footprint and instant startup.

## 🚀 Getting Started

### Prerequisites

- **OS**: Windows 10/11
- **Runtime**: [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (or SDK to build)

### Installation & Run

1. Clone the repository:
   ```bash
   git clone https://github.com/geocorfu/Obviousidian.git
   ```
2. Navigate to the project directory:
   ```bash
   cd Obviousidian
   ```
3. Run the application:
   ```bash
   dotnet run --project Obviousidian.App
   ```

*(Note: Use `config.json` to set your vault path if it differs from default.)*

## 📖 Usage

### 1. Plain Text
- Type your thought in the main text box.
- Press **Save**.
- Filed in: `notes/`
- **Format**: Includes YAML frontmatter (`created_at`, `source`, `tags`).

### 2. Images
- **Clipboard**: Copy an image and click "Paste".
- **Drag & Drop**: Drop an image file (.png, .jpg) onto the window.
- **Safety**: Automatically renames files to avoid overwriting (e.g. `image_1.png`).
- Press **Save**.
- Image saved to `attachments/`, Note saved to `screenshots/`.

### 3. URLs
- Paste a connection (e.g. YouTube, Medium, Blog).
- App **fetches the page title** automatically.
- **Smart Routing**:
  - `videos/`: YouTube, Vimeo.
  - `articles/`: Medium, Dev.to, blog posts.
  - `bookmarks/`: General links.
- Press **Save**.
- **Format**: Markdown link + YAML frontmatter (`url`, `source`).

## ⚙ Configuration
Modify `config.json` to change the destination:
```json
{
  "VaultPath": "C:\\Users\\YourName\\Documents\\ObsidianVault"
}
```

## 🏗 Architecture

- **Core (`Obviousidian.Core`)**: Pure C# library handling domain logic (Routing, Markdown generation, File I/O).
- **App (`Obviousidian.App`)**: WPF application using MVVM pattern.

## 🛣 Roadmap

- [ ] Configurable Vault Path
- [ ] Global Hotkey support (Win+Shift+X)
- [ ] System Tray integration
- [ ] Advanced URL scraping (Title fetching)

## 📄 License

MIT License.
