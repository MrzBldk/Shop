import { getPictureUrl } from 'app/utils';
import { useState } from 'react';
import { FaArrowAltCircleRight, FaArrowAltCircleLeft } from 'react-icons/fa';
import styles from './ImageSlider.module.scss'

export function ImageSlider({ slides }: { slides: Array<string> }) {
    const [current, setCurrent] = useState(0);
    const length = slides.length;

    if (length === 0) {
        return null
    }

    const nextSlide = () => {
        setCurrent(current === length - 1 ? 0 : current + 1);
    };

    const prevSlide = () => {
        setCurrent(current === 0 ? length - 1 : current - 1);
    };

    return (
        <section className={styles['image-slider']}>
            <FaArrowAltCircleLeft className={styles['image-slider__left-arrow']} onClick={prevSlide} />
            <FaArrowAltCircleRight className={styles['image-slider__right-arrow']} onClick={nextSlide} />
            {slides.map((slide, index) => {
                return (
                    <div
                        className={index === current ? styles['image-slider__slide--active'] : styles['image-slider__slide']}
                        key={index}
                    >
                        {index === current && (
                            <img className={styles['image-slider__image']} src={getPictureUrl(slide)} alt='product' />
                        )}
                    </div>
                );
            })}
        </section>
    );
};