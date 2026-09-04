const form = document.getElementById("chatForm");
const input = document.getElementById("messageInput");
const messages = document.getElementById("messages");
const quickReplies = document.getElementById("quickReplies");
const favoritesContainer = document.getElementById("favorites");
const clearFavoritesButton = document.getElementById("clearFavorites");
const favoritesStorageKey = "jenny-favorites";

const starterResponse = {
    reply: "Hi, I’m Jenny. I can help with train-and-ferry routes, things to do, restaurant ideas, weather notes, and places to stay.",
    quickReplies: ["London to Dublin", "Things to do in Paris", "Where should I eat in Amsterdam?"],
    options: []
};

renderMessage("bot", starterResponse.reply, starterResponse);
renderQuickReplies(starterResponse.quickReplies);
renderFavorites();

form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const message = input.value.trim();
    if (!message) {
        return;
    }

    renderMessage("user", message);
    input.value = "";
    input.focus();

    try {
        const response = await fetch("/api/chat", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ message })
        });

        if (!response.ok) {
            throw new Error(`Request failed with status ${response.status}`);
        }

        const payload = await response.json();
        renderMessage("bot", payload.reply, payload);
        renderQuickReplies(payload.quickReplies ?? []);
    } catch (error) {
        renderMessage("bot", "Sorry — I couldn’t process that just now. Please try again in a moment.");
        console.error(error);
    }
});

clearFavoritesButton.addEventListener("click", () => {
    localStorage.removeItem(favoritesStorageKey);
    renderFavorites();
});

function renderMessage(role, text, payload) {
    const wrapper = document.createElement("article");
    wrapper.className = `message ${role}`;

    const paragraph = document.createElement("p");
    paragraph.textContent = text;
    wrapper.appendChild(paragraph);

    if (role === "bot" && payload?.options?.length) {
        payload.options.forEach((option) => wrapper.appendChild(buildOptionCard(option)));
    }

    messages.appendChild(wrapper);
    messages.scrollTop = messages.scrollHeight;
}

function buildOptionCard(option) {
    const card = document.createElement("section");
    card.className = "option-card";

    const title = document.createElement("h3");
    title.textContent = option.title;
    card.appendChild(title);

    const summary = document.createElement("p");
    summary.textContent = option.summary;
    card.appendChild(summary);

    if (option.travelTime || option.costEstimate) {
        const meta = document.createElement("div");
        meta.className = "meta";

        if (option.travelTime) {
            const time = document.createElement("span");
            time.textContent = `Travel time: ${option.travelTime}`;
            meta.appendChild(time);
        }

        if (option.costEstimate) {
            const cost = document.createElement("span");
            cost.textContent = `Estimated cost: ${option.costEstimate}`;
            meta.appendChild(cost);
        }

        card.appendChild(meta);
    }

    if (option.details?.length) {
        const list = document.createElement("ul");
        option.details.forEach((detail) => {
            const item = document.createElement("li");
            item.textContent = detail;
            list.appendChild(item);
        });
        card.appendChild(list);
    }

    const saveButton = document.createElement("button");
    saveButton.type = "button";
    saveButton.className = "save-button";
    saveButton.textContent = "Save";
    saveButton.addEventListener("click", () => saveFavorite(option));
    card.appendChild(saveButton);

    return card;
}

function renderQuickReplies(replies) {
    quickReplies.replaceChildren();

    replies.forEach((reply) => {
        const button = document.createElement("button");
        button.type = "button";
        button.className = "chip";
        button.textContent = reply;
        button.addEventListener("click", () => {
            input.value = reply;
            input.focus();
        });
        quickReplies.appendChild(button);
    });
}

function renderFavorites() {
    favoritesContainer.replaceChildren();

    const favorites = getFavorites();
    if (!favorites.length) {
        const emptyState = document.createElement("p");
        emptyState.className = "empty-state";
        emptyState.textContent = "Save route cards or recommendations here for quick access later.";
        favoritesContainer.appendChild(emptyState);
        return;
    }

    favorites.forEach((favorite) => {
        const card = document.createElement("section");
        card.className = "favorite-card";

        const title = document.createElement("h3");
        title.textContent = favorite.title;
        card.appendChild(title);

        const summary = document.createElement("p");
        summary.textContent = favorite.summary;
        card.appendChild(summary);

        if (favorite.details?.length) {
            const list = document.createElement("ul");
            favorite.details.forEach((detail) => {
                const item = document.createElement("li");
                item.textContent = detail;
                list.appendChild(item);
            });
            card.appendChild(list);
        }

        favoritesContainer.appendChild(card);
    });
}

function saveFavorite(option) {
    const favorites = getFavorites();
    const duplicate = favorites.some((favorite) => favorite.title === option.title && favorite.summary === option.summary);
    if (duplicate) {
        return;
    }

    favorites.unshift(option);
    localStorage.setItem(favoritesStorageKey, JSON.stringify(favorites.slice(0, 8)));
    renderFavorites();
}

function getFavorites() {
    try {
        return JSON.parse(localStorage.getItem(favoritesStorageKey) ?? "[]");
    } catch {
        return [];
    }
}
