import styles from './Spinner.module.scss'

export function Spinner({ text = '', size = '5em' }: { text?: string, size?: string }) {
    const header = text ? <h4>{text}</h4> : null
    return (
        <div className={styles.spinner}>
            {header}
            <div className={styles.spinner__loader} style={{ height: size, width: size }} />
        </div>
    )
}