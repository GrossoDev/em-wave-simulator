import React, { useEffect, useRef } from "react";

const gamePath = "/unity-build/index.html";

const UnityGame = () => {
  const gameContainerRef = useRef(null);

  return (
    <iframe
      src={gamePath}
      ref={gameContainerRef}
      style={{
        width: '100%',
        height: '100%',
        border: 'none',
      }}
      title="Unity WebGL Game"
      allowFullScreen
    />
  );
};

export default UnityGame;
