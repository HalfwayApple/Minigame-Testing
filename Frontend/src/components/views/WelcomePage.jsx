import React from 'react'
import { useNavigate } from 'react-router-dom';

const WelcomePage = () => {
    const navigate = useNavigate()

    const handleClick = () => {
        navigate('/PlayGame')
    }

  return (
    <div>
      <button onClick={() => handleClick()}>Start game</button>
    </div>
  )
}

export default WelcomePage
