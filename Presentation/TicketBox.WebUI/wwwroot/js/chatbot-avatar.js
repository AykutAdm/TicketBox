(function () {
    const PALETTE = {
        dark: '#080000',
        light: '#fff',
        skin: 'hsl(120, 40%, 80%)',
        skinHighlight: 'hsl(120, 40%, 90%)',
        skinShadow: 'hsl(120, 30%, 60%)',
        flesh: 'hsl(120, 50%, 30%)'
    };

    const SCENE_SIZE = 400;

    function createCharacterModel(illo) {
        const Zdog = window.Zdog;
        const headAnchor = new Zdog.Anchor({ addTo: illo, translate: { y: -42 } });
        new Zdog.Group({ addTo: headAnchor });
        new Zdog.Shape({ addTo: headAnchor.children[0], stroke: 228, color: PALETTE.skinShadow, path: [{ x: -4.5 }, { x: 4.5 }] });
        new Zdog.Shape({ addTo: headAnchor.children[0], stroke: 216, color: PALETTE.skin, translate: { x: -4.5 } });

        const eyeAnchor = new Zdog.Anchor({ addTo: headAnchor, translate: { x: -66, y: -30, z: 84 }, rotate: { y: Zdog.TAU / 11 } });
        const eyeGroup = new Zdog.Group({ addTo: eyeAnchor });

        new Zdog.Shape({
            addTo: eyeGroup,
            fill: true,
            stroke: 0,
            color: PALETTE.skinShadow,
            scale: 1.15,
            path: [{ x: 0, y: 0, z: 3 }, { bezier: [{ x: 24, y: 0, z: 3 }, { x: 36, y: 21, z: 0 }, { x: 36, y: 36, z: 0 }] }, { bezier: [{ x: 36, y: 51, z: 0 }, { x: 24, y: 63, z: 3 }, { x: 0, y: 63, z: 3 }] }, { bezier: [{ x: -24, y: 63, z: 3 }, { x: -36, y: 51, z: 0 }, { x: -36, y: 36, z: 0 }] }, { bezier: [{ x: -36, y: 21, z: 0 }, { x: -24, y: 0, z: 3 }, { x: 0, y: 0, z: 3 }] }]
        });

        const eye = new Zdog.Shape({
            addTo: eyeGroup,
            fill: true,
            stroke: 3,
            color: PALETTE.dark,
            translate: { y: 6 },
            path: [{ x: 0, y: 0, z: 3 }, { bezier: [{ x: 24, y: 0, z: 3 }, { x: 36, y: 21, z: 0 }, { x: 36, y: 36, z: 0 }] }, { bezier: [{ x: 36, y: 51, z: 0 }, { x: 24, y: 63, z: 3 }, { x: 0, y: 63, z: 3 }] }, { bezier: [{ x: -24, y: 63, z: 3 }, { x: -36, y: 51, z: 0 }, { x: -36, y: 36, z: 0 }] }, { bezier: [{ x: -36, y: 21, z: 0 }, { x: -24, y: 0, z: 3 }, { x: 0, y: 0, z: 3 }] }]
        });

        eye.copy({
            addTo: eye,
            fill: true,
            color: PALETTE.light,
            scale: 0.4,
            translate: { x: -9, y: 9, z: 3 }
        });

        const eyeLeft = eyeAnchor.copyGraph({ translate: { x: 66, y: -30, z: 84 }, rotate: { y: Zdog.TAU / -11 } });

        const mouthAnchor = new Zdog.Anchor({ addTo: headAnchor, translate: { y: 40, z: 96 }, rotate: { x: Zdog.TAU / -45 } });
        new Zdog.Shape({
            addTo: mouthAnchor,
            stroke: 8,
            fill: false,
            color: PALETTE.skinShadow,
            closed: false,
            path: [
                { x: -28, y: 10, z: 0 },
                { bezier: [{ x: -14, y: 26, z: -2 }, { x: 14, y: 26, z: -2 }, { x: 28, y: 10, z: 0 }] }
            ]
        });
        new Zdog.Shape({
            addTo: mouthAnchor,
            stroke: 5,
            fill: false,
            color: PALETTE.skinHighlight,
            translate: { y: -2, z: 1 },
            closed: false,
            path: [
                { x: -24, y: 12, z: 0 },
                { bezier: [{ x: -12, y: 22, z: -1 }, { x: 12, y: 22, z: -1 }, { x: 24, y: 12, z: 0 }] }
            ]
        });

        const bodyAnchor = new Zdog.Anchor({ addTo: illo, translate: { y: 81 } });
        const bodyGroup = new Zdog.Group({ addTo: bodyAnchor });
        const bodyUpperGroup = new Zdog.Group({ addTo: bodyGroup });
        const bodyUpper = new Zdog.Shape({ addTo: bodyUpperGroup, stroke: 63, fill: true, color: PALETTE.skinShadow, translate: { y: 6 } });
        bodyUpper.copy({ stroke: 57, color: PALETTE.skin, translate: { x: -3 } });

        const armGroup = new Zdog.Group({ addTo: bodyAnchor, translate: { z: -6 }, rotate: { x: Zdog.TAU / 16 } });
        const arm = new Zdog.Shape({ addTo: armGroup, stroke: 30, color: PALETTE.skinShadow, path: [{ x: -35, y: -6, z: 0 }, { bezier: [{ x: -33, y: -6, z: 0 }, { x: -45, y: -6, z: 0 }, { x: -54, y: 30, z: 0 }] }], closed: false });
        arm.copy({ stroke: 27, color: PALETTE.skin });
        const armLeft = armGroup.copyGraph({ rotate: { x: Zdog.TAU / 16, y: Zdog.TAU / 2 } });
        armLeft.children[1].stroke = 21;
        armLeft.children[1].translate = { x: 1, y: 1 };

        const bodyLowerGroup = new Zdog.Group({ addTo: bodyGroup, translate: { y: 30 } });
        new Zdog.Shape({ addTo: bodyLowerGroup, stroke: 69, fill: true, color: PALETTE.skinShadow, translate: { y: 6 }, path: [{ x: -4.5 }, { x: 4.5 }] }).copy({ stroke: 66, color: PALETTE.skin, translate: { x: -3, y: 4.5 }, path: [{ x: -4.5 }, { x: 4.5 }] });

        const legGroup = new Zdog.Group({ addTo: illo, translate: { y: 141, z: -3 } });
        new Zdog.Shape({ addTo: legGroup, stroke: 28, color: PALETTE.skinShadow, translate: { y: 6 }, path: [{ x: -21, y: -6, z: 0 }, { bezier: [{ x: -18, y: -6, z: 0 }, { x: -24, y: -6, z: 0 }, { x: -24, y: 24, z: 0 }] }], closed: false }).copy({ stroke: 24, color: PALETTE.skin });
        const footGroup = new Zdog.Group({ addTo: legGroup, translate: { x: -25, y: 42, z: 4 }, rotate: { x: Zdog.TAU / 4 } });
        new Zdog.Hemisphere({ addTo: footGroup, stroke: 5, diameter: 23, color: PALETTE.skinShadow, backface: PALETTE.skinShadow }).copy({ diameter: 20, color: PALETTE.skin, backface: PALETTE.skin, translate: { y: -2, z: 2 } });
        const legLeft = legGroup.copyGraph({ rotate: { y: Zdog.TAU / 2 } });
        legLeft.children[1].stroke = 20;
        legLeft.children[1].translate = { x: 1, y: 9 };
        legLeft.children[2].translate = { x: -25, y: 42, z: -4 };

        return {
            headAnchor: headAnchor,
            bodyAnchor: bodyAnchor,
            bodyUpper: bodyUpper,
            eyeRight: eye,
            eyeLeft: eyeLeft.children[0]
        };
    }

    function initChatbotAvatar() {
        const canvas = document.getElementById('chatbot-avatar-canvas');
        const btn = document.getElementById('user-chatbot-btn');
        const zone = document.getElementById('user-chatbot-zone');
        if (!canvas || !zone || !window.Zdog || !window.TweenMax) return;

        const Zdog = window.Zdog;
        const TweenMax = window.TweenMax;
        const TweenLite = window.TweenLite;
        const Sine = window.Sine;

        const illo = new Zdog.Illustration({
            element: canvas,
            dragRotate: true,
            zoom: 0.34,
            translate: { y: 12 }
        });

        const model = createCharacterModel(illo);
        let animationFrameId;
        let lookAroundTimeout;
        let mouseTimeout;

        TweenMax.to(model.bodyUpper.scale, 0.5, { x: 0.95, y: 0.97, repeat: -1, yoyo: true, ease: Sine.easeInOut });

        function blink() {
            const randomDelay = Math.random() * 6 + 2;
            TweenMax.to([model.eyeRight.scale, model.eyeLeft.scale], 0.07, {
                y: 0,
                repeat: 1,
                yoyo: true,
                delay: randomDelay,
                onComplete: blink
            });
        }
        blink();

        function lookAround() {
            const randomY = (Math.random() * 40 - 20) / 360 * Zdog.TAU;
            const randomDuration = Math.random() + 0.5;
            TweenLite.to(model.headAnchor.rotate, randomDuration, { y: randomY, ease: Sine.easeInOut });
            TweenLite.to(model.bodyAnchor.rotate, randomDuration, {
                y: randomY / 2,
                ease: Sine.easeInOut,
                onComplete: function () {
                    lookAroundTimeout = setTimeout(lookAround, Math.random() * 1000 + 500);
                }
            });
        }
        lookAround();

        function watchPlayer(x, y) {
            const rect = canvas.getBoundingClientRect();
            const rotX = (x - (rect.left + rect.width / 2)) / Zdog.TAU;
            const rotY = -(y - (rect.top + rect.height / 2)) / Zdog.TAU;
            TweenMax.to(model.headAnchor.rotate, 0.5, { x: rotY / 100, y: -rotX / 100, ease: Sine.easeOut });
            TweenMax.to(model.bodyAnchor.rotate, 0.5, { x: rotY / 200, y: -rotX / 200, ease: Sine.easeOut });
        }

        function resetAll() {
            TweenLite.to(model.headAnchor.rotate, 0.5, { x: 0, y: 0, ease: Sine.easeOut });
            TweenLite.to(model.bodyAnchor.rotate, 0.5, { x: 0, y: 0, ease: Sine.easeOut });
            lookAround();
        }

        const TRACK_PAD = { top: 96, right: 14, bottom: 6, left: 112 };
        let isTracking = false;

        function isInsideTrackZone(clientX, clientY) {
            const rect = zone.getBoundingClientRect();
            return clientX >= rect.left - TRACK_PAD.left &&
                clientX <= rect.right + TRACK_PAD.right &&
                clientY >= rect.top - TRACK_PAD.top &&
                clientY <= rect.bottom + TRACK_PAD.bottom;
        }

        function onMouseMove(e) {
            if (!isInsideTrackZone(e.clientX, e.clientY)) {
                if (isTracking) {
                    isTracking = false;
                    clearTimeout(mouseTimeout);
                    resetAll();
                }
                return;
            }

            isTracking = true;
            TweenLite.killTweensOf(model.headAnchor.rotate);
            TweenLite.killTweensOf(model.bodyAnchor.rotate);
            clearTimeout(lookAroundTimeout);
            watchPlayer(e.clientX, e.clientY);
            clearTimeout(mouseTimeout);
            mouseTimeout = setTimeout(resetAll, 2000);
        }

        document.addEventListener('mousemove', onMouseMove);

        function animate() {
            illo.updateRenderGraph();
            animationFrameId = requestAnimationFrame(animate);
        }
        animate();

        btn.addEventListener('click', function () {
            btn.classList.toggle('is-active');
        });
    }

    function loadScript(src, callback) {
        if (document.querySelector('script[src="' + src + '"]')) {
            callback();
            return;
        }
        const script = document.createElement('script');
        script.src = src;
        script.onload = callback;
        document.body.appendChild(script);
    }

    document.addEventListener('DOMContentLoaded', function () {
        let loaded = 0;
        function tryInit() {
            loaded++;
            if (loaded === 2) initChatbotAvatar();
        }
        loadScript('https://unpkg.com/zdog@1/dist/zdog.dist.min.js', tryInit);
        loadScript('https://cdnjs.cloudflare.com/ajax/libs/gsap/2.1.3/TweenMax.min.js', tryInit);
    });
})();
