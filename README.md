# Jenny

A .NET/C# chatbot travel assistant for activity recommendations, route planning, and restaurant suggestions.

Jenny is structured as a .NET 8 solution with separate Web, Core, Data, and Tests projects. The current MVP provides a browser-based chatbot UI plus travel APIs for locations, activities, routes, restaurants, chat history, and favorites using seeded in-memory data.

## Run locally

```bash
cd /home/runner/work/Jenny/Jenny/Jenny.Web
mkdir -p /tmp/.dotnet/shm
DOTNET_CLI_HOME=/tmp dotnet run
```

Then open the local URL shown in the console.

## Test

```bash
cd /home/runner/work/Jenny/Jenny
mkdir -p /tmp/.dotnet/shm
DOTNET_CLI_HOME=/tmp dotnet test Jenny.slnx
```

## Deploy to Fly.io

1. Install and authenticate Fly CLI on your machine:
   ```bash
   fly auth login
   ```
2. Update `/home/runner/work/Jenny/Jenny/fly.toml` and replace `your-jenny-app` with your Fly app name.
3. From `/home/runner/work/Jenny/Jenny`, deploy:
   ```bash
   fly deploy
   ```
4. Open the public site:
   ```bash
   fly open
   ```

Jenny is configured for Fly to:
- build from the repository `Dockerfile`
- serve the ASP.NET app on port `8080`
- use `/api/health` for health checks
- allow machines to stop when idle to help minimize cost
