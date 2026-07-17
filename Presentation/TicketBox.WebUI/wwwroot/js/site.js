// ===== Scroll reveal for ticket cards =====
(function () {
    const items = document.querySelectorAll('.ticket');
    if (!items.length) return;
    const io = new IntersectionObserver((entries) => {
        entries.forEach((entry, i) => {
            if (entry.isIntersecting) {
                setTimeout(() => entry.target.classList.add('in-view'), (i % 3) * 90);
                io.unobserve(entry.target);
            }
        });
    }, { threshold: 0.15 });
    items.forEach(t => io.observe(t));
})();

// ===== Category chip active state =====
(function () {
    document.querySelectorAll('.chip-row').forEach(row => {
        row.addEventListener('click', (e) => {
            const chip = e.target.closest('.chip');
            if (!chip) return;
            row.querySelectorAll('.chip').forEach(c => c.classList.remove('active'));
            chip.classList.add('active');
        });
    });
})();

// ===== Mobile nav toggle =====
(function () {
    const toggle = document.querySelector('.nav-toggle');
    const links = document.querySelector('.nav-links');
    if (!toggle || !links) return;
    toggle.addEventListener('click', () => {
        links.classList.toggle('open');
    });
})();

// ===== Hero slider (index page) =====
(function () {
    const slider = document.querySelector('.slider');
    if (!slider) return;

    const slides = [...slider.querySelectorAll('.slide')];
    const dotsWrap = slider.querySelector('.slider-dots');
    const prevBtn = slider.querySelector('.slider-arrow--prev');
    const nextBtn = slider.querySelector('.slider-arrow--next');
    let current = 0;
    let timer;

    slides.forEach((_, i) => {
        const dot = document.createElement('button');
        dot.className = 'slider-dot' + (i === 0 ? ' active' : '');
        dot.setAttribute('aria-label', `${i + 1}. slayta git`);
        dot.addEventListener('click', () => goTo(i));
        dotsWrap.appendChild(dot);
    });
    const dots = [...dotsWrap.querySelectorAll('.slider-dot')];

    function goTo(index) {
        slides[current].classList.remove('active');
        dots[current].classList.remove('active');
        current = (index + slides.length) % slides.length;
        slides[current].classList.add('active');
        dots[current].classList.add('active');
        restart();
    }

    function restart() {
        clearInterval(timer);
        timer = setInterval(() => goTo(current + 1), 6000);
    }

    prevBtn?.addEventListener('click', () => goTo(current - 1));
    nextBtn?.addEventListener('click', () => goTo(current + 1));

    restart();
})();

// ===== Quantity stepper (event detail booking widget) =====
(function () {
    const widget = document.querySelector('.booking-widget');
    if (!widget) return;

    const decrease = widget.querySelector('[data-step="down"]');
    const increase = widget.querySelector('[data-step="up"]');
    const countEl = widget.querySelector('.count');
    const totalEl = widget.querySelector('[data-total]');
    const unitPrice = parseFloat(widget.dataset.price || '0');
    const maxQty = parseInt(widget.dataset.max || '10', 10);
    let qty = 1;

    function render() {
        countEl.textContent = qty;
        if (totalEl) totalEl.textContent = (qty * unitPrice).toLocaleString('tr-TR') + '₺';
        decrease.disabled = qty <= 1;
        increase.disabled = qty >= maxQty;
    }

    decrease?.addEventListener('click', () => { if (qty > 1) qty--; render(); });
    increase?.addEventListener('click', () => { if (qty < maxQty) qty++; render(); });

    render();
})();