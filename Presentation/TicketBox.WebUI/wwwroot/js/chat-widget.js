(function () {
    document.addEventListener('DOMContentLoaded', function () {
        const btn = document.getElementById('user-chatbot-btn');
        const panel = document.getElementById('user-chatbot-panel');
        const closeBtn = document.getElementById('user-chatbot-close');
        const form = document.getElementById('user-chatbot-form');
        const input = document.getElementById('user-chatbot-input');
        const messages = document.getElementById('user-chatbot-messages');
        const moodButtons = document.querySelectorAll('.mood-chip');
        const eventSelect = document.getElementById('user-chatbot-event-select');

        if (!btn || !panel || !form) return;

        let selectedMood = '';
        let typingEl = null;
        let isWaiting = false;

        btn.addEventListener('click', function () {
            panel.classList.toggle('is-open');
        });
        closeBtn.addEventListener('click', function () {
            panel.classList.remove('is-open');
        });

        moodButtons.forEach(function (chip) {
            chip.addEventListener('click', function () {
                moodButtons.forEach(c => c.classList.remove('active'));
                chip.classList.add('active');
                selectedMood = chip.dataset.mood;
            });
        });

        if (eventSelect && window.__ticketBoxToken) {
            fetch('https://localhost:7171/api/Events', {
                headers: { 'Authorization': 'Bearer ' + window.__ticketBoxToken }
            })
                .then(res => res.json())
                .then(events => {
                    events.forEach(function (ev) {
                        const option = document.createElement('option');
                        option.value = ev.eventId;
                        option.textContent = ev.title;
                        eventSelect.appendChild(option);
                    });
                })
                .catch(err => console.error('Etkinlik listesi alınamadı:', err));
        }

        const connection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:7171/chatHub')
            .withAutomaticReconnect()
            .build();

        connection.on('ReceiveMessage', function (reply) {
            hideTyping();
            appendMessage(reply, 'bot');
        });

        connection.start().catch(function (err) {
            console.error('Chatbot bağlantı hatası:', err);
        });

        form.addEventListener('submit', function (e) {
            e.preventDefault();
            const text = input.value.trim();
            if (!text || isWaiting) return;

            appendMessage(text, 'user');
            input.value = '';
            showTyping();

            const selectedEventId = eventSelect && eventSelect.value ? parseInt(eventSelect.value, 10) : null;

            connection.invoke('SendMessage', text, selectedMood, selectedEventId).catch(function (err) {
                hideTyping();
                appendMessage('Bağlantı sorunu oldu, tekrar dener misin?', 'bot');
                console.error(err);
            });
        });

        function showTyping() {
            isWaiting = true;
            input.disabled = true;
            form.querySelector('button').disabled = true;

            typingEl = document.createElement('div');
            typingEl.className = 'chat-msg chat-msg--bot chat-msg--typing';
            typingEl.innerHTML = 'Düşünüyor<span class="chat-typing-dots"><span></span><span></span><span></span></span>';
            messages.appendChild(typingEl);
            messages.scrollTop = messages.scrollHeight;
        }

        function hideTyping() {
            isWaiting = false;
            input.disabled = false;
            form.querySelector('button').disabled = false;

            if (typingEl) {
                typingEl.remove();
                typingEl = null;
            }
        }

        function appendMessage(text, sender) {
            const bubble = document.createElement('div');
            bubble.className = 'chat-msg chat-msg--' + sender;
            bubble.textContent = text;
            messages.appendChild(bubble);
            messages.scrollTop = messages.scrollHeight;
        }
    });
})();
