document.addEventListener('DOMContentLoaded', function () {
    const svg = document.getElementById('pp-connections');
    if (!svg) return;

    function getCenter(el) {
        const rect = el.getBoundingClientRect();
        const svgRect = svg.getBoundingClientRect();
        return {
            x: rect.left + rect.width / 2 - svgRect.left,
            y: rect.top + rect.height / 2 - svgRect.top
        };
    }



    function getEdgePoint(el, side) {
        const rect = el.getBoundingClientRect();
        const svgRect = svg.getBoundingClientRect();
        switch (side) {
            case 'right': return { x: rect.right - svgRect.left, y: rect.top + rect.height / 2 - svgRect.top };
            case 'left': return { x: rect.left - svgRect.left, y: rect.top + rect.height / 2 - svgRect.top };
            case 'bottom': return { x: rect.left + rect.width / 2 - svgRect.left, y: rect.bottom - svgRect.top };
            case 'top': return { x: rect.left + rect.width / 2 - svgRect.left, y: rect.top - svgRect.top };
        }
    }

    function createConnection(x1, y1, x2, y2, id) {
        const midX = x1 + (x2 - x1) / 2;

        const path = document.createElementNS('http://www.w3.org/2000/svg', 'path');
        const d = `M ${x1} ${y1} L ${midX} ${y1} L ${midX} ${y2} L ${x2} ${y2}`;
        path.setAttribute('d', d);
        path.setAttribute('class', 'pp-connection-line');
        path.setAttribute('id', `line-${id}`);

        const dot1 = document.createElementNS('http://www.w3.org/2000/svg', 'circle');
        dot1.setAttribute('cx', x1);
        dot1.setAttribute('cy', y1);
        dot1.setAttribute('r', '3');
        dot1.setAttribute('class', 'pp-connection-dot');

        const dot2 = document.createElementNS('http://www.w3.org/2000/svg', 'circle');
        dot2.setAttribute('cx', x2);
        dot2.setAttribute('cy', y2);
        dot2.setAttribute('r', '3');
        dot2.setAttribute('class', 'pp-connection-dot');

        const pulse = document.createElementNS('http://www.w3.org/2000/svg', 'circle');
        pulse.setAttribute('r', '4');
        pulse.setAttribute('class', 'pp-pulse-dot');

        const animateMotion = document.createElementNS('http://www.w3.org/2000/svg', 'animateMotion');
        animateMotion.setAttribute('dur', `${2 + id * 0.5}s`);
        animateMotion.setAttribute('repeatCount', 'indefinite');
        animateMotion.setAttribute('keyPoints', '0;1');
        animateMotion.setAttribute('keyTimes', '0;1');
        animateMotion.setAttribute('calcMode', 'linear');

        const mpath = document.createElementNS('http://www.w3.org/2000/svg', 'mpath');
        mpath.setAttributeNS('http://www.w3.org/1999/xlink', 'xlink:href', `#line-${id}`);

        animateMotion.appendChild(mpath);
        pulse.appendChild(animateMotion);

        svg.appendChild(path);
        svg.appendChild(dot1);
        svg.appendChild(dot2);
        svg.appendChild(pulse);
    }

    function drawConnections() {
        svg.innerHTML = '';

        const avatar = document.getElementById('pp-avatar-center');
        const collection = document.getElementById('pp-collection-block');
        const comments = document.getElementById('pp-comments-block');
        const identity = document.getElementById('pp-identity-block');
        const stats = document.getElementById('pp-stats-block');

        if (!avatar || !collection || !comments || !identity || !stats) return;

        const center = getCenter(avatar);

        const collectionPt = getEdgePoint(collection, 'right');
        const commentsPt = getEdgePoint(comments, 'left');
        const identityPt = getEdgePoint(identity, 'top');
        const statsPt = getEdgePoint(stats, 'top');

        createConnection(center.x, center.y, collectionPt.x, collectionPt.y, 0);
        createConnection(center.x, center.y, commentsPt.x, commentsPt.y, 1);
        createConnection(center.x, center.y, identityPt.x, identityPt.y, 2);
        createConnection(center.x, center.y, statsPt.x, statsPt.y, 3);
    }

    drawConnections();
    window.addEventListener('resize', drawConnections);
});