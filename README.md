# CS467_ML_Breakout
A Unity ML-Agents Breakout clone where a reinforcement learning paddle agent learns to play Atari-style Breakout, with a human-vs-machine comparison mode.

## Creating a Pull Request

Follow these steps whenever you start a new task.

### 1. Update your local `main` branch

```bash
git checkout main
git pull origin main
```

### 2. Create a new feature branch

Choose a descriptive branch name.

```bash
git checkout -b feature/your-feature-name
```

Examples:

* `feature/paddle-movement`
* `feature/ml-agent`
* `bugfix/ball-collision`

### 3. Make your changes

Work on your assigned task in your feature branch.

### 4. Format your code

Before committing, format your code to ensure it follows the project's `.editorconfig` rules.

```bash
dotnet format
```

### 5. Commit your changes

```bash
git add .
git commit -m "Add a descriptive commit message"
```

### 6. Push your branch

The first time you push a new branch:

```bash
git push -u origin feature/your-feature-name
```

For future pushes to the same branch:

```bash
git push
```

### 7. Open a Pull Request

On GitHub:

* Go to **Pull requests**
* Click **New pull request** (or use the **Compare & pull request** button)
* Set:

  * **Base:** `main`
  * **Compare:** `feature/your-feature-name`

Include a brief summary of your changes and create the Pull Request.

### 8. Code Review

At least one teammate should review the Pull Request before it is merged. The CI checks should pass before merging.

### 9. After the Pull Request is merged

Switch back to `main`, pull the latest changes, and create a **new feature branch** for your next task.

```bash
git checkout main
git pull origin main
```

Do not continue working on a branch after its Pull Request has been merged.
