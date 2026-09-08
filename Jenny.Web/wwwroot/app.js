const storageKey = "jenny-user-id";
const chatForm = document.getElementById("chat-form");
const input = document.getElementById("message-input");
const messages = document.getElementById("chat-messages");
const statusText = document.getElementById("status-text");
const sessionLabel = document.getElementById("session-label");
const resetButton = document.getElementById("reset-chat");
const quickActions = document.getElementById("quick-actions");

let userId = loadUserId();

sessionLabel.textContent = `Session ${userId.slice(0, 8)}`;

chatForm.addEventListener("submit", async (event) => {
    event.preventDefault();
    const message = input.value.trim();
    if (!message) {
        return;
    }

    appendMessage("user", "You", message);
    input.value = "";
    await sendMessage(message);
});

resetButton.addEventListener("click", () => {
    userId = crypto.randomUUID();
    localStorage.setItem(storageKey, userId);
    sessionLabel.textContent = `Session ${userId.slice(0, 8)}`;
    messages.innerHTML = "";
    appendMessage("assistant", "Jenny", "Started a new chat. Ask me about routes, food, activities, or tips.");
    statusText.textContent = "New session ready";
});

quickActions.addEventListener("click", async (event) => {
    const button = event.target.closest("button[data-prompt]");
    if (!button) {
        return;
    }

    input.value = button.dataset.prompt ?? "";
    input.focus();
});

void loadHistory();

async function loadHistory() {
    try {
        setBusy("Loading chat history...");
        const response = await fetch(`/api/chat/history?userId=${encodeURIComponent(userId)}`);
        if (!response.ok) {
            throw new Error("Unable to load chat history.");
        }

        const history = await response.json();
        if (!Array.isArray(history.messages) || history.messages.length === 0) {
            setReady("Ready");
            return;
        }

        messages.innerHTML = "";
        for (const message of history.messages) {
            appendMessage(
                String(message.sender).toLowerCase() === "user" ? "user" : "assistant",
                String(message.sender) === "User" ? "You" : "Jenny",
                message.content,
                String(message.type).toLowerCase() === "clarification"
            );
        }

        setReady("History loaded");
    } catch (error) {
        appendMessage("assistant", "Jenny", error.message ?? "Unable to load chat history.", true);
        setReady("Ready");
    }
}

async function sendMessage(message) {
    try {
        setBusy("Jenny is thinking...");
        const response = await fetch("/api/chat", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                userId,
                message
            })
        });

        if (!response.ok) {
            const error = await response.json().catch(() => null);
            throw new Error(error?.detail ?? "Jenny could not process that message.");
        }

        const payload = await response.json();
        appendMessage("assistant", "Jenny", payload.reply, Boolean(payload.requiresClarification));
        setReady(payload.requiresClarification ? "Jenny needs more detail" : "Ready");
    } catch (error) {
        appendMessage("assistant", "Jenny", error.message ?? "Something went wrong.", true);
        setReady("Ready");
    }
}

function appendMessage(role, label, content, isClarification = false) {
    const article = document.createElement("article");
    article.className = `message ${role}${isClarification ? " clarification" : ""}`;

    const meta = document.createElement("p");
    meta.className = "message-meta";
    meta.textContent = label;

    const body = document.createElement("p");
    body.textContent = content;

    article.append(meta, body);
    messages.appendChild(article);
    messages.scrollTop = messages.scrollHeight;
}

function loadUserId() {
    const existing = localStorage.getItem(storageKey);
    if (existing) {
        return existing;
    }

    const created = crypto.randomUUID();
    localStorage.setItem(storageKey, created);
    return created;
}

function setBusy(message) {
    statusText.textContent = message;
}

function setReady(message) {
    statusText.textContent = message;
}
