import React from 'react'
import { useNavigate } from 'react-router-dom';
import './WelcomePage.css';  // Make sure to import the CSS file

const WelcomePage = () => {
    const navigate = useNavigate()

    const handleClick = () => {
        setTimeout(() => {
            navigate('/PlayGame');
        }, 500);
    }

    return (
        <div className="welcome-container">
            <button className="button" id='startgame-button' onClick={() => handleClick()}>Start Game</button>
        </div>
    )
}

export default WelcomePage
