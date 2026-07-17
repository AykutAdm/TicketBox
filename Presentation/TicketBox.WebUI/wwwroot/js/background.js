// ===================================================
// Animated background — organic flowing gradient (WebGL)
// Ported from a React component to plain JS for MVC/Razor use.
// Renders into <canvas id="bg-canvas"> which must sit inside
// the fixed, full-screen .stage-bg wrapper.
// ===================================================

(function () {
    const VERT = `
    attribute vec2 position;
    varying vec2 vUv;
    void main() {
      vUv = position * 0.5 + 0.5;
      gl_Position = vec4(position, 0.0, 1.0);
    }
  `;

    const FRAG = `
    precision highp float;
    varying vec2 vUv;
 
    uniform vec2  u_resolution;
    uniform float u_time;
    uniform float u_grain;
    uniform vec3  u_colors[4];
    uniform vec3  u_bg;
 
    vec3 permute(vec3 x) { return mod(((x*34.0)+1.0)*x, 289.0); }
 
    float snoise(vec2 v){
      const vec4 C = vec4(0.211324865405187, 0.366025403784439,
               -0.577350269189626, 0.024390243902439);
      vec2 i  = floor(v + dot(v, C.yy) );
      vec2 x0 = v -   i + dot(i, C.xx);
      vec2 i1 = (x0.x > x0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);
      vec4 x12 = x0.xyxy + C.xxzz;
      x12.xy -= i1;
      i = mod(i, 289.0);
      vec3 p = permute( permute( i.y + vec3(0.0, i1.y, 1.0 ))
      + i.x + vec3(0.0, i1.x, 1.0 ));
      vec3 m = max(0.5 - vec3(dot(x0,x0), dot(x12.xy,x12.xy),
        dot(x12.zw,x12.zw)), 0.0);
      m = m*m ;
      m = m*m ;
      vec3 x = 2.0 * fract(p * C.www) - 1.0;
      vec3 h = abs(x) - 0.5;
      vec3 ox = floor(x + 0.5);
      vec3 a0 = x - ox;
      m *= 1.79284291400159 - 0.85373472095314 * ( a0*a0 + h*h );
      vec3 g;
      g.x  = a0.x  * x0.x  + h.x  * x0.y;
      g.yz = a0.yz * x12.xz + h.yz * x12.yw;
      return 130.0 * dot(m, g);
    }
 
    void main() {
      vec2 uv = vUv;
      float ratio = u_resolution.x / u_resolution.y;
      vec2 p = uv - 0.5;
      p.x *= ratio;
 
      float t = u_time * 0.3;
      vec2 drift = vec2(sin(t * 0.62), cos(t * 0.54)) * 0.24;
      vec2 drift2 = vec2(cos(t * 0.4), sin(t * 0.7)) * 0.16;
      float pulse = sin(t * 0.55) * 0.07;
 
      float n1 = snoise(p * 0.36 + drift + vec2(t * 0.58, -t * 0.65));
      float n2 = snoise(p * 0.5 + drift * 1.4 + drift2 + vec2(-t * 0.48, t * 0.55) + n1 * 0.38);
      float n3 = snoise(p * 0.68 + drift * 0.75 - drift2 + vec2(t * 0.4, -t * 0.52) + n2 * 0.32);
 
      vec3 col = u_bg;
 
      float dist = length(p) * 1.5;
      float vignette = 1.0 - smoothstep(0.3, 1.2, dist);
 
      col = mix(col, u_colors[0], smoothstep(-0.15 + pulse, 0.42 + pulse, n1) * 0.85);
      col = mix(col, u_colors[1], smoothstep(-0.05 - pulse, 0.55 - pulse * 0.5, n2) * 0.7);
      col = mix(col, u_colors[2], smoothstep(-0.25 + pulse * 0.5, 0.38, n3) * 0.55);
      col = mix(col, u_colors[3], smoothstep(0.0, 0.65, n1 * n2) * 0.45);
 
      float glow = smoothstep(0.8, 0.0, dist) * (0.32 + sin(t * 0.7) * 0.08);
      col += u_colors[0] * glow;
 
      col = mix(col * 0.28, col, vignette);
 
      float grain = fract(sin(dot(uv, vec2(12.9898, 78.233))) * 43758.5453 + u_time);
      col += (grain - 0.5) * u_grain * 0.1;
 
      gl_FragColor = vec4(col, 1.0);
    }
  `;

    function hexToRgb(hex) {
        const h = hex.replace('#', '');
        return [
            parseInt(h.slice(0, 2), 16) / 255,
            parseInt(h.slice(2, 4), 16) / 255,
            parseInt(h.slice(4, 6), 16) / 255
        ];
    }

    function initBackground(canvas, opts) {
        const gl = canvas.getContext('webgl');
        if (!gl) {
            // WebGL not available: hide canvas, the solid --ink background
            // of .stage-bg stays visible as a safe fallback.
            canvas.style.display = 'none';
            return;
        }

        function createShader(type, src) {
            const s = gl.createShader(type);
            gl.shaderSource(s, src);
            gl.compileShader(s);
            if (!gl.getShaderParameter(s, gl.COMPILE_STATUS)) {
                console.warn('TicketBox background shader error:', gl.getShaderInfoLog(s));
            }
            return s;
        }

        const program = gl.createProgram();
        gl.attachShader(program, createShader(gl.VERTEX_SHADER, VERT));
        gl.attachShader(program, createShader(gl.FRAGMENT_SHADER, FRAG));
        gl.linkProgram(program);
        gl.useProgram(program);

        const buffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array([-1, -1, 1, -1, -1, 1, 1, 1]), gl.STATIC_DRAW);

        const posLoc = gl.getAttribLocation(program, 'position');
        gl.enableVertexAttribArray(posLoc);
        gl.vertexAttribPointer(posLoc, 2, gl.FLOAT, false, 0, 0);

        const locs = {
            res: gl.getUniformLocation(program, 'u_resolution'),
            time: gl.getUniformLocation(program, 'u_time'),
            grain: gl.getUniformLocation(program, 'u_grain'),
            colors: gl.getUniformLocation(program, 'u_colors'),
            bg: gl.getUniformLocation(program, 'u_bg')
        };

        function resize() {
            const dpr = Math.min(window.devicePixelRatio || 1, 2);
            canvas.width = canvas.clientWidth * dpr;
            canvas.height = canvas.clientHeight * dpr;
            gl.viewport(0, 0, canvas.width, canvas.height);
        }
        const ro = new ResizeObserver(resize);
        ro.observe(canvas);
        resize();

        const colorFlat = new Float32Array(opts.colors.flatMap(hexToRgb));
        const bgRgb = hexToRgb(opts.bg);

        let raf;
        function render(t) {
            gl.uniform2f(locs.res, canvas.width, canvas.height);
            gl.uniform1f(locs.time, t * 0.001 * opts.speed);
            gl.uniform1f(locs.grain, opts.grain);
            gl.uniform3f(locs.bg, bgRgb[0], bgRgb[1], bgRgb[2]);
            gl.uniform3fv(locs.colors, colorFlat);
            gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);
            raf = requestAnimationFrame(render);
        }
        raf = requestAnimationFrame(render);

        // stop the loop if the tab is hidden — saves battery/CPU
        document.addEventListener('visibilitychange', () => {
            if (document.hidden) {
                cancelAnimationFrame(raf);
            } else {
                raf = requestAnimationFrame(render);
            }
        });
    }

    document.addEventListener('DOMContentLoaded', function () {
        const canvas = document.getElementById('bg-canvas');
        if (!canvas) return;

        const prefersReduced = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
        if (prefersReduced) {
            canvas.style.display = 'none';
            return;
        }

        initBackground(canvas, {
            bg: '#07070E',
            colors: ['#3B82F6', '#2563EB', '#1D4ED8', '#0B1120'], // darker blue with brighter moving highlights
            speed: 1.6,
            grain: 0.15
        });
    });
})();