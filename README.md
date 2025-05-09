# GitBug: The GitHub Contribution Bug Hunter

A strategic game that transforms GitHub's contribution grid into a bug-hunting challenge - a reverse-minesweeper where more commits mean more bugs!

![GitBug Screenshot](https://example.com/gitbug-screenshot.png)

## 🎮 Game Concept

In GitBug, you're a QA engineer tasked with finding bugs in a codebase. The catch? Areas with higher commit activity are more likely to contain bugs. You must strategically deploy your limited testing resources to find all bugs before running out.

As developers know, the more code changes, the higher chance of introducing bugs - making this game both fun and surprisingly realistic!

## 🕹️ How to Play

### Controls
- **Left-click**: Run a full test on a cell (expensive but definitive)
- **Right-click**: Mark a cell as "bug-free" (costless note-taking)
- **Middle-click**: Perform static analysis (cheaper but less reliable)

### Game Elements
- **Resources**: Each test costs resources; static analysis costs less but is less reliable
- **Finding Bugs**: Successfully finding a bug restores some resources
- **Architecture Types**: Choose between different complexity levels:
  - **Monolith**: Simple structure, lower testing cost (5 per test)
  - **Microservices**: More complex with more potential bugs (7 per test)
  - **Spaghetti Code**: Highly complex with many bugs hiding in dense code (10 per test)

### Visual Indicators
- **Gray cells** (varying shades): Untested code, darker means more commits
- **Blue cells**: Tested areas
- **Green cells**: Areas adjacent to safe cells
- **Amber cells**: Manually marked as "safe"
- **Red cells**: Found bugs
- **Numbers**: Indicate how many bugs are adjacent to a safe cell

## 📊 GitHub Integration

GitBug uses GitHub usernames to generate unique game boards based on real contribution patterns:

1. The game can simulate contribution patterns for any GitHub username
2. Select a year to see that user's activity transformed into a bug-hunting playground
3. More active users will have more complex grids with potentially more bugs

## 🚀 Usage

GitBug is designed to replace "github" in a profile URL:

```
https://github.com/torvalds → https://gitbug.com/torvalds
```

You can also try it with these popular developers:
- torvalds (Linux creator)
- gaearon (React developer)
- yyx990803 (Vue.js creator)
- sindresorhus (Prolific open-source developer)

Or me ^^
- LaM0uette

## 💻 Installation & Development

### Prerequisites
- .NET 9 SDK
- A compatible IDE (Visual Studio 2022+ or VS Code with C# extensions)

### Setup
```bash
# Clone the repository
git clone https://github.com/yourusername/gitbug.git

# Navigate to the project directory
cd gitbug

# Restore dependencies
dotnet restore

# Run the application
dotnet watch run
```

### Deployment
```bash
# Build for production
dotnet publish -c Release
```

## 🛠️ Technology Stack

- **Blazor WebAssembly** - .NET 9
- **C#** - Primary language
- **ASP.NET Core** - Backend API (for GitHub integration)
- **CSS & Bootstrap** - Styling

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🙏 Acknowledgements

- Inspired by GitHub's contribution grid
- Built with Blazor .NET 9
