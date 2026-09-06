# Jenny
A .NET/C# chatbot travel assistant for activity recommendations, route planning, and restaurant suggestions

## Run locally

```bash
cd /home/runner/work/Jenny/Jenny/Jenny.Web
dotnet run
```

Open `http://localhost:5099/`.

## Deploy to Fly.io

1. Install and authenticate Fly CLI on your machine:
   ```bash
   fly auth login
   ```
2. Update `/home/runner/work/Jenny/Jenny/fly.toml` and replace `your-jenny-app` with a globally unique Fly app name.
3. From `/home/runner/work/Jenny/Jenny`, create the app if needed:
   ```bash
   fly apps create your-jenny-app
   ```
4. Deploy:
   ```bash
   fly deploy
   ```
5. Open the public site:
   ```bash
   fly open
   ```

Jenny is configured for Fly to:
- build from the repository `Dockerfile`
- serve the ASP.NET app on port `8080`
- use `/api/health` for health checks
- allow machines to stop when idle to help minimize cost
